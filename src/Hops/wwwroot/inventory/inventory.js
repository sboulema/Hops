var Inventory = {
    Hops: [],
    Malts: [],
    Yeasts: [],
    Recipes: []
};

function save(element) {
    var hopInvCode = localStorage.getItem("firebaseCode");
    if (hopInvCode === null) {
        if ($(element).data("hop-id") !== undefined) {
            Inventory.Hops.push({ id: $(element).data("hop-id"), amount: 0 });
        }
        if ($(element).data("malt-id") !== undefined) {
            Inventory.Malts.push({ id: $(element).data("malt-id"), amount: 0 });
        }
        if ($(element).data("yeast-id") !== undefined) {
            Inventory.Yeasts.push({ id: $(element).data("yeast-id"), amount: 0 });
        }

        saveToFirebase(Inventory);
    }
    else {
        loadFromFirebase(function(inventory) {
            if ($(element).data("hop-id") !== undefined) {
                var foundAtIndex = isFoundAtIndex($(element).data("hop-id"), inventory.Hops);

                if (foundAtIndex > -1) {
                    inventory.Hops.splice(foundAtIndex, 1);
                } else {
                    inventory.Hops.push({ id: $(element).data("hop-id"), amount: 0 });
                }
            }
            if ($(element).data("malt-id") !== undefined) {
                var foundAtIndex = isFoundAtIndex($(element).data("malt-id"), inventory.Malts);

                if (foundAtIndex > -1) {
                    inventory.Malts.splice(foundAtIndex, 1);
                } else {
                    inventory.Malts.push({ id: $(element).data("malt-id"), amount: 0 });
                }
            }
            if ($(element).data("yeast-id") !== undefined) {
                var foundAtIndex = isFoundAtIndex($(element).data("yeast-id"), inventory.Yeasts);

                if (foundAtIndex > -1) {
                    inventory.Yeasts.splice(foundAtIndex, 1);
                } else {
                    inventory.Yeasts.push({ id: $(element).data("yeast-id"), amount: 0 });
                }
            }
            saveToFirebase(inventory);
        });     
    }

    $(element).toggleClass("fas fal");
}

function saveRecipe(id, name, date, beerxml) {
    loadFromFirebase(function (inventory) {
        if (typeof inventory.Recipes === 'undefined') {
            inventory.Recipes = [];
        }

        var foundAtIndex = isFoundAtIndex(id, inventory.Recipes);
        if (foundAtIndex > -1) {
            inventory.Recipes.splice(foundAtIndex, 1);
        }

        inventory.Recipes.push({ id: id, name: name, date: date, beerxml: beerxml });
        saveToFirebase(inventory);
    });
}

function loadRecipesFromInventory() {
    loadFromFirebase(function (inventory) {
        $.each(inventory.Recipes, function (index, recipe) {
            $("#recipesTable").append("<tr><td><a href='" + recipe.id + "'>" + recipe.name + "</a><td><td>" + new Date(recipe.date * 1000).toISOString() + "<td></tr>")
        });
    });
}

function getRecipe(id) {
    loadFromFirebase(function (inventory) {
        var foundAtIndex = isFoundAtIndex(id, inventory.Recipes);
        return inventory.Recipes[foundAtIndex];
    }); 
}

function loadFromInventory() {
    loadFromFirebase(function (inventory) {
        $(".hop-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("hop-id"), inventory.Hops);
            if (foundAtIndex > -1) {
                $(this).toggleClass("fas fal");
                $(".hop-inv-amount[data-hop-id='" + $(this).data("hop-id") + "']").val(inventory.Hops[foundAtIndex].amount);
            }
        });
        $(".malt-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("malt-id"), inventory.Malts);
            if (foundAtIndex > -1) {
                $(this).toggleClass("fas fal");
                $(".malt-inv-amount[data-malt-id='" + $(this).data("malt-id") + "']").val(inventory.Malts[foundAtIndex].amount);
            }
        });
        $(".yeast-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("yeast-id"), inventory.Yeasts);
            if (foundAtIndex > -1) {
                $(this).toggleClass("fas fal");
                $(".yeast-inv-amount[data-yeast-id='" + $(this).data("yeast-id") + "']").val(inventory.Yeasts[foundAtIndex].amount);
            }
        });
    });
}

function saveAmountInventory(element) {
    loadFromFirebase(function (inventory) {
        if ($(element).data("hop-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("hop-id"), inventory.Hops);

            if (foundAtIndex > -1) {
                inventory.Hops[foundAtIndex].amount = parseInt(element.value);
            }
        }
        if ($(element).data("malt-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("malt-id"), inventory.Malts);

            if (foundAtIndex > -1) {
                inventory.Malts[foundAtIndex].amount = parseInt(element.value);
            }
        }
        if ($(element).data("yeast-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("yeast-id"), inventory.Yeasts);

            if (foundAtIndex > -1) {
                inventory.Yeasts[foundAtIndex].amount = parseInt(element.value);
            }
        }
        saveToFirebase(inventory);
    });
}

function loadInventory(page) {
    loadFromFirebase(function (inventory) {
        if (inventory.Hops.length > 0) {
            $.get("/hop/inventory/" + inventory.Hops.map(function (v) { return v.id; }).join() + "/" + page, function (data) {
                $(".hop-inv").html(data);
            });
        }
        else {
            $(".inv").html("<div class='alert alert-info' role='alert'>No hops to be found :(</div>");
        }
        if (inventory.Malts.length > 0) {
            $.get("/malt/inventory/" + inventory.Malts.map(function (v) { return v.id; }).join() + "/" + page, function (data) {
                $(".malt-inv").html(data);
            });
        }
        else {
            $(".inv").html("<div class='alert alert-info' role='alert'>No malts to be found :(</div>");
        }
        if (inventory.Yeasts.length > 0) {
            $.get("/yeast/inventory/" + inventory.Yeasts.map(function (v) { return v.id; }).join() + "/" + page, function (data) {
                $(".yeast-inv").html(data);
            });
        }
        else {
            $(".inv").html("<div class='alert alert-info' role='alert'>No yeasts to be found :(</div>");
        }
    });
}

function loadInventoryCode() {
    $("#invCode").val(getFirebaseCode());
}

function deleteInventory() {
    localStorage.clear();
    location.reload();
}

function exportInventory() {
    loadFromFirebase(function (inventory) {
        downloadURI('data:text/plain;charset=utf-8,' + encodeURIComponent(JSON.stringify(inventory)), "HopsInventory.json");
    });
}

function downloadURI(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
}

function importInventory(event) {
    importToFireBase(event);
}

function syncInventory() {
    localStorage.setItem("firebaseCode", $("#invCode").val());
    location.reload();
}

function isFound(id, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i].id === id) {
            return true;
        }
    }
    return false;
}

function isFoundAtIndex(id, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i].id === id) {
            return i;
        }
    }
    return -1;
}