function save(element) {
    var Inventory = {
        Hops: [],
        Malts: [],
        Yeasts: []
    };

    var hopInvCode = localStorage.getItem("hopInvCode");

    if (hopInvCode === null) {
        if ($(element).data("hop-id") !== undefined) {
            Inventory.Hops.push($(element).data("hop-id"));
        }
        if ($(element).data("malt-id") !== undefined) {
            Inventory.Malts.push($(element).data("malt-id"));
        }
        if ($(element).data("yeast-id") !== undefined) {
            Inventory.Yeasts.push($(element).data("yeast-id"));
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
            var foundAtIndex = $.inArray($(element).data("hop-id"), Inventory.Hops);

            if (foundAtIndex > -1) {
                Inventory.Hops.splice(foundAtIndex, 1);
            } else {
                Inventory.Hops.push($(element).data("hop-id"));
            }
        }
        if ($(element).data("malt-id") !== undefined) {
            var foundAtIndex = $.inArray($(element).data("malt-id"), Inventory.Malts);

            if (foundAtIndex > -1) {
                Inventory.Malts.splice(foundAtIndex, 1);
            } else {
                Inventory.Malts.push($(element).data("malt-id"));
            }
        }
        if ($(element).data("yeast-id") !== undefined) {
            var foundAtIndex = $.inArray($(element).data("yeast-id"), Inventory.Yeasts);

            if (foundAtIndex > -1) {
                Inventory.Yeasts.splice(foundAtIndex, 1);
            } else {
                Inventory.Yeasts.push($(element).data("yeast-id"));
            }
        }

        $.ajax({
            url: hopInvCode,
            type: "PUT",
            data: JSON.stringify(Inventory),
            contentType: " application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                // items saved to inventory
            }
        });
    }

    $(element).toggleClass("glyphicon-star glyphicon-star-empty");
}

function loadFromInventory() {
    $.get(localStorage.getItem("hopInvCode"), function (Inventory, textStatus, jqXHR) {
        $(".hop-inv").each(function (index) {
            var foundAtIndex = $.inArray($(this).data("hop-id"), Inventory.Hops);

            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
            }
        });
        $(".malt-inv").each(function (index) {
            var foundAtIndex = $.inArray($(this).data("malt-id"), Inventory.Malts);

            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
            }
        });
        $(".yeast-inv").each(function (index) {
            var foundAtIndex = $.inArray($(this).data("yeast-id"), Inventory.Yeasts);

            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
            }
        });
    });
}

function loadInventory(page) {
    var hopInvCode = localStorage.getItem("hopInvCode");

    if (hopInvCode !== null) {
        $.get(hopInvCode, function (Inventory, textStatus, jqXHR) {
            if (Inventory.Hops.length > 0) {
                $.get("/hop/inventory/" + Inventory.Hops.join() + "/" + page, function (data) {
                    $(".hop-inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No hops to be found :(</div>");
            }
            if (Inventory.Malts.length > 0) {
                $.get("/malt/inventory/" + Inventory.Malts.join() + "/" + page, function (data) {
                    $(".malt-inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No malts to be found :(</div>");
            }
            if (Inventory.Yeasts.length > 0) {
                $.get("/yeast/inventory/" + Inventory.Yeasts.join() + "/" + page, function (data) {
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