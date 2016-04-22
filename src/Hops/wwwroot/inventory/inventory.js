var Inventory = {
    Hops: [],
    Malts: [],
    Yeasts: []
};

function save(element) {
    var hopInvCode = localStorage.getItem("hopInvCode");
    if (hopInvCode === null) {
        if ($(element).data("hop-id") !== undefined) {
            Inventory.Hops.push([$(element).data("hop-id"), 0]);
        }
        if ($(element).data("malt-id") !== undefined) {
            Inventory.Malts.push([$(element).data("malt-id"), 0]);
        }
        if ($(element).data("yeast-id") !== undefined) {
            Inventory.Yeasts.push([$(element).data("yeast-id"), 0]);
        }
        
        $.ajax({
            url: "https://api.myjson.com/bins",
            type: "POST",
            data: JSON.stringify(Inventory),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                localStorage.setItem("hopInvCode", data.uri);
            }
        });
    }
    else {
        $.ajax({
            url: hopInvCode,
            type: "GET",
            success: function (data) {
                Inventory = data;
            },
            async: false
        });

        if ($(element).data("hop-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("hop-id"), Inventory.Hops);

            if (foundAtIndex > -1) {
                Inventory.Hops.splice(foundAtIndex, 1);
            } else {
                Inventory.Hops.push([$(element).data("hop-id"), 0]);
            }
        }
        if ($(element).data("malt-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("malt-id"), Inventory.Malts);

            if (foundAtIndex > -1) {
                Inventory.Malts.splice(foundAtIndex, 1);
            } else {
                Inventory.Malts.push([$(element).data("malt-id"), 0]);
            }
        }
        if ($(element).data("yeast-id") !== undefined) {
            var foundAtIndex = isFoundAtIndex($(element).data("yeast-id"), Inventory.Yeasts);

            if (foundAtIndex > -1) {
                Inventory.Yeasts.splice(foundAtIndex, 1);
            } else {
                Inventory.Yeasts.push([$(element).data("yeast-id"), 0]);
            }
        }

        saveInventory(Inventory);
    }

    $(element).toggleClass("glyphicon-star glyphicon-star-empty");
}

function loadFromInventory() {
    var hopInvCode = localStorage.getItem("hopInvCode");
    $.get(localStorage.getItem("hopInvCode"), function (Inventory, textStatus, jqXHR) {
        $(".hop-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("hop-id"), Inventory.Hops);
            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
                $(".hop-inv-amount[data-hop-id='" + $(this).data("hop-id") + "']").val(Inventory.Hops[foundAtIndex][1]);
            }
        });
        $(".malt-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("malt-id"), Inventory.Malts);
            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
                $(".malt-inv-amount[data-malt-id='" + $(this).data("malt-id") + "']").val(Inventory.Malts[foundAtIndex][1]);
            }
        });
        $(".yeast-inv").each(function (index) {
            var foundAtIndex = isFoundAtIndex($(this).data("yeast-id"), Inventory.Yeasts);
            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
                $(".yeast-inv-amount[data-yeast-id='" + $(this).data("yeast-id") + "']").val(Inventory.Yeasts[foundAtIndex][1]);
            }
        });
    });
}

function saveAmountInventory(element) {
    var Inventory = getInventory();

    if ($(element).data("hop-id") !== undefined) {
        var foundAtIndex = isFoundAtIndex($(element).data("hop-id"), Inventory.Hops);

        if (foundAtIndex > -1) {
            Inventory.Hops[foundAtIndex][1] = parseInt(element.value);
        }
    }
    if ($(element).data("malt-id") !== undefined) {
        var foundAtIndex = isFoundAtIndex($(element).data("malt-id"), Inventory.Malts);

        if (foundAtIndex > -1) {
            Inventory.Malts[foundAtIndex][1] = parseInt(element.value);
        }
    }
    if ($(element).data("yeast-id") !== undefined) {
        var foundAtIndex = isFoundAtIndex($(element).data("yeast-id"), Inventory.Yeasts);

        if (foundAtIndex > -1) {
            Inventory.Yeasts[foundAtIndex][1] = parseInt(element.value);
        }
    }

    saveInventory(Inventory);
}

function loadInventory(page) {
    var hopInvCode = localStorage.getItem("hopInvCode");

    if (hopInvCode !== null) {
        $.get(hopInvCode, function (Inventory, textStatus, jqXHR) {
            if (Inventory.Hops.length > 0) {
                $.get("/hop/inventory/" + Inventory.Hops.map(function(v){return v[0];}).join() + "/" + page, function (data) {
                    $(".hop-inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No hops to be found :(</div>");
            }
            if (Inventory.Malts.length > 0) {
                $.get("/malt/inventory/" + Inventory.Malts.map(function (v) { return v[0]; }).join() + "/" + page, function (data) {
                    $(".malt-inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No malts to be found :(</div>");
            }
            if (Inventory.Yeasts.length > 0) {
                $.get("/yeast/inventory/" + Inventory.Yeasts.map(function (v) { return v[0]; }).join() + "/" + page, function (data) {
                    $(".yeast-inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No yeasts to be found :(</div>");
            }
        });
    }
}

function loadInventoryCode() {
    var hopInvCode = localStorage.getItem("hopInvCode");
    if (hopInvCode !== null) {
        $("#invCode").val(hopInvCode.split("/").pop());
    }  
}

function deleteInventory() {
    localStorage.clear();
    location.reload();
}

function exportInventory() {
    $.get(localStorage.getItem("hopInvCode"), function (data, textStatus, jqXHR) {
        downloadURI('data:text/plain;charset=utf-8,' + encodeURIComponent(JSON.stringify(data)), "HopsInventory.txt");
    });
}

function downloadURI(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
}

function importInventory(element) {
    var file = element.files[0];

    if (file) {
        var r = new FileReader();
        r.onload = function (e) {
            var contents = e.target.result;

            $.ajax({
                url: "https://api.myjson.com/bins",
                type: "POST",
                data: contents,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    localStorage.setItem("hopInvCode", data.uri);
                    location.reload();
                }
            });        
        }
        r.readAsText(file);
    } else {
        alert("Failed to load file");
    }
}

function syncInventory() {
    localStorage.setItem("hopInvCode", "https://api.myjson.com/bins/" + $("#invCode").val());
    location.reload();
}

function isFound(id, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i][0] === id) {
            return true;
        }
    }
    return false;
}

function isFoundAtIndex(id, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i][0] === id) {
            return i;
        }
    }
    return -1;
}

function getInventory() {
    var hopInvCode = localStorage.getItem("hopInvCode");
    var inventory = [];
    $.ajax({
        url: hopInvCode,
        type: "GET",
        success: function (data) {
            inventory = data;
        },
        async: false
    });
    return inventory;
}

function saveInventory(inventory) {
    var hopInvCode = localStorage.getItem("hopInvCode");
    $.ajax({
        url: hopInvCode,
        type: "PUT",
        data: JSON.stringify(inventory),
        contentType: " application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jqXHR) {
            // items saved to inventory
        }
    });
}