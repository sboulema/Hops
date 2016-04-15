function save(element) {
    var hopInv = [];

    var hopInvCode = localStorage.getItem("hopInvCode");

    if (hopInvCode === null) {
        hopInv.push($(element).data("hop-id"));
        $.ajax({
            url: "https://api.myjson.com/bins",
            type: "POST",
            data: JSON.stringify(hopInv),
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
                hopInv = data;
            },
            async: false
        });

        var foundAtIndex = $.inArray($(element).data("hop-id"), hopInv);

        if (foundAtIndex > -1) {
            hopInv.splice(foundAtIndex, 1);
        } else {
            hopInv.push($(element).data("hop-id"));
        }

        $.ajax({
            url: hopInvCode,
            type: "PUT",
            data: JSON.stringify(hopInv),
            contentType: " application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                // Hop saved to inventory
            }
        });
    }

    $(element).toggleClass("glyphicon-star glyphicon-star-empty");
}

function loadFromInventory() {
    $.get(localStorage.getItem("hopInvCode"), function (hopInv, textStatus, jqXHR) {
        $(".hop-inv").each(function (index) {
            var foundAtIndex = $.inArray($(this).data("hop-id"), hopInv);

            if (foundAtIndex > -1) {
                $(this).toggleClass("glyphicon-star glyphicon-star-empty");
            }
        });
    });
}

function loadInventory(page) {
    var hopInvCode = localStorage.getItem("hopInvCode");

    if (hopInvCode !== null) {
        $.get(hopInvCode, function (hopInv, textStatus, jqXHR) {
            if (hopInv.length > 0) {
                $.get("/search/partial/inventory/" + hopInv.join() + "/" + page, function (data) {
                    $(".inv").html(data);
                });
            }
            else {
                $(".inv").html("<div class='alert alert-info' role='alert'>No hops to be found :(</div>");
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