var recipe;

function createBar(value, scaleStart, scaleEnd, lowerBound, upperBound) {
    var barStart = ((lowerBound - scaleStart) / (scaleEnd - scaleStart)) * 100;
    var barEnd = ((upperBound - scaleStart) / (scaleEnd - scaleStart)) * 100;
    var valueMarker = ((value - scaleStart) / (scaleEnd - scaleStart)) * 100;

    var bar = "";
    bar += "<div class='progress' style='margin: 0px 0px 5px 0px;'>";
    bar += "<div class='progress-bar' role='progressbar' style='background-color: #f5f5f5; background-image: linear-gradient(to bottom,#ebebeb 0,#f5f5f5 100%); box-shadow: inset 0 1px 2px rgba(0,0,0,.1); width: " + barStart + "%'><span class='sr-only'>bla</span></div>";
    bar += "<div class='progress-bar' role='progressbar' style='background-image: linear-gradient(to bottom,#b55002 0,#b55002 100%); opacity: 0.2; width: " + (barEnd - barStart) + "%'><span class='sr-only'>bla</span></div>";
    bar += "<div style='width: 3px; height: 20px; position: relative; background: #ce5b02; left: " + valueMarker + "%;'></div>";
    bar += "</div>";
    return bar;
}

function calcBugu(og, ibu) {
    var bugu = ibu / ((og - 1) * 1000);
    if (isNaN(bugu) || bugu < 0) bugu = 0;
    if (bugu > 1) bugu = 1;
    return bugu;
}

function calcBv(og, fg, ibu, isUpper) {
    var rte = (0.82 * (fg - 1.000) + 0.18 * (og - 1.000)) * 1000.0;
    var bv = 0.8 * ibu / rte;
    if (isNaN(bv) || bv < 0) bv = isUpper ? 1 : 0;
    if (bv > 1) bv = 1;
    return bv;
}

function isConform(recipe) {
    var result = true;
    result = result && (recipe.og >= recipe.style.og[0] && recipe.og <= recipe.style.og[1]);
    result = result && (recipe.fg >= recipe.style.fg[0] && recipe.fg <= recipe.style.fg[1]);
    result = result && (recipe.ibu >= recipe.style.ibu[0] && recipe.ibu <= recipe.style.ibu[1]);
    result = result && (recipe.color >= recipe.style.color[0] && recipe.color <= recipe.style.color[1]);
    result = result && (recipe.abv >= recipe.style.abv[0] && recipe.abv <= recipe.style.abv[1]);
    return result;
}

function addFermentable() {
    var index = $("#fermentables").children().length;
    $("#fermentables").append("<tr id='malt-" + index + "' data-id='" + index + "'>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
        "</div>" +
        "</td>" +
        "<td width='70%'>" +
        "<input id='malt-" + index + "-name' type='text' class='form-control malt typeahead'>" +
        "</td>" +
        "<td>" +
        "<select class='custom-select'>" +
        "<option value='mash'>Mash</option>" +
        "<option value='steep'>Steep</option>" +
        "<option value='boil'>Boil</option>" +
        "</select>" +
        "</td>" +
        "<td id='malt-" + index + "-yield'></td>" +
        "<td></td>" +
        "<td id='malt-" + index + "-color'></td>" +
        "<td>" +
        "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delFermentable(" + index + ");'>" +
        " <i class='fas fa-minus'></i>" +
        "</button>" +
        "</td>" +
        "</tr>");
    loadMaltTypeahead();
}

function delFermentable(id) {
    $("#malt-" + id).remove();
}

function addHop() {
    var index = $("#hops").children().length + 1;
    $("#hops").append("<tr id='hop-" + index + "' data-id='" + index + "'>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
        "</div>" +
        "</td>" +
        "<td>" + "<input id='hop-" + index + "-name' type='text' class='form-control hop typeahead'>" + "</td>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>m</span></div>" +
        "</div>" +
        "</td>" +
        "<td>" +
        "<select class='custom-select'>" +
        "<option value='boil'>Boil</option>" +
        "<option value='primary'>Primary</option>" +
        "<option value='secondary'>Secondary</option>" +
        "</select>" +
        "</td>" +
        "<td>" +
        "<select class='custom-select'>" +
        "<option value='pellets'>Pellets</option>" +
        "<option value='leafs'>Leafs</option>" +
        "<option value='plugs'>Plugs</option>" +
        "</select>" +
        "</td>" +
        "<td id='hop-" + index + "-aa'></td>" +
        "<td>" +
        "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delHop(" + index + ");'>" +
        " <i class='fas fa-minus'></i>" +
        "</button>" +
        "</td>" +
        "</tr>");
    loadHopTypeahead();
}

function delHop(id) {
    $("#hop-" + id).remove();
}

function addExtra() {
    var index = $("#extras").children().length + 1;
    $("#extras").append("<tr id='extra-" + index + "'>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
        "</div>" +
        "</td>" +
        "<td>" + "<input type='text' class='form-control'>" + "</td>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>m</span></div>" +
        "</div>" +
        "</td>" +
        "<td>" +
        "<select class='custom-select'>" +
        "<option value='boil'>Boil</option>" +
        "<option value='primary'>Primary</option>" +
        "<option value='secondary'>Secondary</option>" +
        "</select>" +
        "</td>" +
        "<td>" +
        "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delExtra(" + index + ");'>" +
        " <i class='fas fa-minus'></i>" +
        "</button>" +
        "</td>" +
        "</tr>");
}

function delExtra(id) {
    $("#extra-" + id).remove();
}

function addYeast() {
    var index = $("#yeasts").children().length;
    $("#yeasts").append("<tr id='yeast-" + index + "' data-id='" + index + "'>" +
        "<td>" +
        "<div class='input-group'>" +
        "<input type='text' class='form-control'>" +
        "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
        "</div>" +
        "</td>" +
        "<td><input id='yeast-" + index + "-name' type='text' class='form-control yeast typeahead'></td>" +
        "<td id='yeast-" + index + "-lab'></td>" +
        "<td id='yeast-" + index + "-att'></td>" +
        "<td>" +
        "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delYeast(" + index + ");'>" +
        " <i class='fas fa-minus'></i>" +
        "</button>" +
        "</td>" +
        "</tr>");
    loadYeastTypeahead();
}

function delYeast(id) {
    $("#yeast-" + id).remove();
}

function toBeerXml() {
    var recipe = new Brauhaus.Recipe({
        name: $('<div />').html($("#recipeName").val()).html(),
        author: $('<div />').html($("#recipeAuthor").val()).html(),
        description: $('<div />').html($("#recipeDescription").val()).html(),
        batchSize: parseFloat($("#batchSize").val()),
        boilSize: 1.7,
        boilTime: parseFloat($("#boilTime").val()),
        style: Brauhaus.getStyle($("#recipeStyle").val().split("---")[0], $("#recipeStyle").val().split("---")[1]),
        type: $("#recipeType").val(),
        date: Date.parse($("#recipeDate").val()),
        og: $("#recipeOG").val(),
        fg: $("#recipeFG").val(),
        tasteNotes: $("#recipeTastingNotes").val(),
        tasteRating: parseFloat($("#recipeRating").val()) * 10,
        bottlingPressure: parseFloat($("#carbonationLevel").val()),
        forcedCarbonation: $("#forcedCarbonation").prop('checked'),
        primingSugarName: $("#primingSugarName").val()
    });

    $("#fermentables tr").each(function (index) {
        var index = $(this).data("id");
        recipe.add('fermentable', {
            name: document.getElementById("malt-" + index + "-name").value,
            color: Brauhaus.lovibondToSrm(parseFloat($(this).find("#malt-" + index + "-color").text())),
            weight: parseFloat($(this).find("input")[0].value) / 1000,
            yield: parseFloat($(this).find("#malt-" + index + "-yield").text())
        });
    });

    $("#hops tr").each(function (index) {
        var index = $(this).data("id");
        recipe.add('spice', {
            name: document.getElementById("hop-" + index + "-name").value,
            weight: parseFloat($(this).find("input")[0].value) / 1000,
            aa: parseFloat($(this).find("#hop-" + index + "-aa").text()),
            use: $(this).find("select")[0].value,
            form: $(this).find("select")[1].value,
            time: parseFloat($(this).find("input")[3].value)
        });
    });

    $("#yeasts tr").each(function (index) {
        var index = $(this).data("id");
        recipe.add('yeast', {
            name: document.getElementById("yeast-" + index + "-name").value,
            type: 'ale',
            form: 'dry',
            attenuation: parseFloat($(this).find("#yeast-" + index + "-att").text()),
            weight: parseFloat($(this).find("input")[0].value) / 1000,
            laboratory: $(this).find("#yeast-" + index + "-lab").text()
        });
    });

    $("#extras tr").each(function (index) {
        recipe.add('spice', {
            name: $(this).find("input")[1].value,
            weight: parseFloat($(this).find("input")[0].value) / 1000,
            use: $(this).find("select")[0].value,
            time: parseFloat($(this).find("input")[2].value)
        });
    });

    return recipe.toBeerXml();
}

function createStyleList(recipeStyleName) {
    var categories = Brauhaus.getStyleCategories();
    $.each(categories, function (index, category) {
        $("#recipeStyle").append("<option disabled>-- " + category + " --</option>");
        var styles = Brauhaus.getStyles(category);
        $.each(styles, function (index, style) {
            $("#recipeStyle").append("<option value='" + category + "---" + style + "'" + (recipeStyleName === style ? "selected" : "") + ">" + style + "</option>");
        });
    });
}

function calculate(recipeId, beerXml) {
    recipe = Brauhaus.Recipe.fromBeerXml(beerXml)[0];
    recipe.ibuMethod = "rager";

    var measuredOG = recipe.og;
    var measuredFG = recipe.fg;

    recipe.calculate();

    $("#name").text(recipe.name);

    $("#style").text("a " + recipe.type + " " + recipe.style.name + " by " + recipe.author);

    $("#stats").empty();
    $("#stats").append("<tr><td style='width:6%;'>OG</td>     <td style='width:6%;'><strong>" + recipe.og.toFixed(3) + "</strong></td><td width='88%'>" + createBar(recipe.og, 1.000, 1.150, recipe.style.og[0], recipe.style.og[1]) + "</td></tr>");
    $("#stats").append("<tr><td style='width:6%;'>FG</td>     <td style='width:6%;'><strong>" + recipe.fg.toFixed(3) + "</strong></td><td width='88%'>" + createBar(recipe.fg, 1.000, 1.150, recipe.style.fg[0], recipe.style.fg[1]) + "</td></tr>");
    $("#stats").append("<tr><td style='width:6%;'>IBU</td>    <td style='width:6%;'><strong>" + recipe.ibu.toFixed(0) + "</strong></td><td width='88%'>" + createBar(recipe.ibu, 0, 120, recipe.style.ibu[0], recipe.style.ibu[1]) + "</td></tr>");
    $("#stats").append("<tr><td style='width:6%;'>SRM</td>    <td style='width:6%;'><strong>" + recipe.color.toFixed(0) + "</strong></td><td width='88%'>" + createBar(recipe.color, 0, 40, recipe.style.color[0], recipe.style.color[1]) + "</td></tr>");
    $("#stats").append("<tr><td style='width:6%;'>ABV</td>    <td style='width:6%;'><strong>" + recipe.abv.toFixed(1) + "%</strong></td><td width='88%'>" + createBar(recipe.abv, 1, 14, recipe.style.abv[0], recipe.style.abv[1]) + "</td></tr>");

    recipe.ibuMethod = "tinseth";
    recipe.calculate();

    $("#stats").append("<tr><td style='width:6%;'>Balance</td><td style='width:6%;'><strong>" + recipe.bv.toFixed(2) + "</strong></td><td width='88%'>" + createBar(recipe.bv, 0, 1, calcBugu(recipe.style.og[0], recipe.style.ibu[0]), calcBugu(recipe.style.og[0], recipe.style.ibu[1])) + "</td></tr>");
    $("#stats").append("<tr class='d-print-none'><td style='width:6%;'></td><td style='width:6%;'></td><td style='width:88%;'><h5><small class='text-muted'>" + (isConform(recipe) ? "<span class='fas fa-check' aria-hidden='true'></span> " : "") + "Recipe " + (isConform(recipe) ? "conforms" : "does not conform") + " to the <strong>" + recipe.style.name + "</strong> style.</small><h5> </td></tr>");

    $("#fermentables").empty();
    $.each(recipe.fermentables, function (index, fermentable) {
        $("#fermentables").append("<tr id='malt-" + index + "' + data-id='" + index + "'>" +
            "<td>" +
            "<div class='input-group'>" +
            "<input type='text' class='form-control' value=" + (fermentable.weight * 1000) + ">" +
            "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
            "</div>" +
            "</td>" +
            "<td>" +
            "<input id='malt-" + index + "-name' type='text' class='form-control malt typeahead' onkeypress='handle(event, search)' value='" + fermentable.name + "'>" +
            "</td>" +
            "<td>" +
            "<select class='custom-select'>" +
            "<option value='mash'" + (fermentable.addition().toLowerCase() === "mash" ? " selected" : "") + ">Mash</option>" +
            "<option value='steep'" + (fermentable.addition().toLowerCase() === "steep" ? " selected" : "") + ">Steep</option>" +
            "<option value='boil'" + (fermentable.addition().toLowerCase() === "boil" ? " selected" : "") + ">Boil</option>" +
            "</select>" +
            "</td>" +
            "<td id='malt-" + index + "-yield'>" + fermentable.yield.toFixed(0) + "%</td>" +
            "<td>" + Math.ceil(fermentable.ppg()) + "</td>" +
            "<td id='malt-" + index + "-color'>" + fermentable.color + " &deg;L</td>" +
            "<td>" +
            "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delFermentable(" + index + ");'>" +
            " <i class='fas fa-minus'></i>" +
            "</button>" +
            "</td>" +
            "</tr>");
    });

    $("#hops").empty();
    $("#extras").empty();
    var hopIndex = 0;
    var extraIndex = 0;
    $.each(recipe.spices, function (index, spice) {
        if (spice.aa !== 0) {
            $("#hops").append("<tr id='hop-" + hopIndex + "' data-id='" + hopIndex + "'>" +
                "<td>" +
                "<div class='input-group'>" +
                "<input type='text' class='form-control' value=" + (spice.weight * 1000) + ">" +
                "<div class='input-group-append'><span class='input-group-text'>g</span></div>" +
                "</div>" +
                "</td>" +
                "<td>" + "<input id='hop-" + hopIndex + "-name' type='text' class='form-control hop typeahead' onkeypress='handle(event, search)' value='" + spice.name + "'>" + "</td>" +
                "<td>" +
                "<div class='input-group'>" +
                "<input type='text' class='form-control' value=" + spice.time + ">" +
                "<div class='input-group-append'><span class='input-group-text'>m</span></div>" +
                "</div>" +
                "</td>" +
                "<td>" +
                "<select class='custom-select'>" +
                "<option value='boil'" + (spice.use.toLowerCase() === "boil" ? " selected" : "") + ">Boil</option>" +
                "<option value='primary'" + (spice.use.toLowerCase() === "primary" ? " selected" : "") + ">Primary</option>" +
                "<option value='secondary'" + (spice.use.toLowerCase() === "secondary" ? " selected" : "") + ">Secondary</option>" +
                "</select>" +
                "</td>" +
                "<td>" +
                "<select class='custom-select'>" +
                "<option value='pellets'" + (spice.form.toLowerCase() === "pellets" ? " selected" : "") + ">Pellets</option>" +
                "<option value='leafs'" + (spice.form.toLowerCase() === "leafs" ? " selected" : "") + ">Leafs</option>" +
                "<option value='plugs'" + (spice.form.toLowerCase() === "plugs" ? " selected" : "") + ">Plugs</option>" +
                "</select>" +
                "</td>" +
                "<td id='hop-" + hopIndex + "-aa'>" + spice.aa.toFixed(1) + "%</td>" +
                "<td>" +
                "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delHop(" + hopIndex + ");'>" +
                " <i class='fas fa-minus'></i>" +
                "</button>" +
                "</td>" +
                "</tr>");
            hopIndex++;
        } else {
            $("#extras").append("<tr id='extra-" + extraIndex + "' data-id='" + extraIndex + "'>" +
                "<td>" +
                "<div class='input-group'>" +
                "<input type='text' class='form-control' value=" + (spice.weight * 1000) + ">" +
                "<div class='input-group-append'><span class='input-group-text'>" + (spice.amountIsWeight === false ? " L" : " g") + "</span></div>" +
                "</div>" +
                "</td>" +
                "<td>" + "<input type='text' class='form-control' value='" + spice.name + "'>" + "</td>" +
                "<td>" +
                "<div class='input-group'>" +
                "<input type='text' class='form-control' value=" + spice.time + ">" +
                "<div class='input-group-append'><span class='input-group-text'>m</span></div>" +
                "</div>" +
                "</td>" +
                "<td>" +
                "<select class='custom-select'>" +
                "<option value='boil'" + (spice.use.toLowerCase() === "boil" ? " selected" : "") + ">Boil</option>" +
                "<option value='primary'" + (spice.use.toLowerCase() === "primary" ? " selected" : "") + ">Primary</option>" +
                "<option value='secondary'" + (spice.use.toLowerCase() === "secondary" ? " selected" : "") + ">Secondary</option>" +
                "</select>" +
                "</td>" +
                "<td>" +
                "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delExtra(" + extraIndex + ");'>" +
                " <i class='fas fa-minus'></i>" +
                "</button>" +
                "</td>" +
                "</tr>");
            extraIndex++;
        }
    });

    $("#yeasts").empty();
    $.each(recipe.yeast, function (index, yeast) {
        $("#yeasts").append("<tr id='yeast-" + index + "' data-id='" + index + "'>" +
            "<td>" +
            "<div class='input-group'>" +
            "<input type='text' class='form-control' value=" + (yeast.weight * 1000) + ">" +
            "<div class='input-group-append'><span class='input-group-text'>" + (yeast.amountIsWeight === false ? " L" : " g") + "</span></div>" +
            "</div>" +
            "</td>" +
            "<td><input id='yeast-" + index + "-name' type='text' class='form-control yeast typeahead' onkeypress='handle(event, search)' value='" + yeast.name + "'></td>" +
            "<td id='yeast-" + index + "-lab'>" + yeast.laboratory + "</td>" +
            "<td id='yeast-" + index + "-att'>" + yeast.attenuation + "%</td>" +
            "<td>" +
            "<button type='button' class='btn btn-sm btn-outline-danger d-print-none' onclick='delYeast();'>" +
            " <i class='fas fa-minus'></i>" +
            "</button>" +
            "</td>" +
            "</tr>");
    });

    $("#batchSize").val(recipe.batchSize);
    $("#boilTime").val(recipe.boilTime);
    $("#recipeName").val(recipe.name);
    $("#recipeAuthor").val(recipe.author);
    $("#recipeDescription").val(recipe.description);
    $("#recipeType").val(recipe.type);
    $("#recipeId").val(recipeId);

    $("#recipeStyle").empty();
    createStyleList(recipe.style.name);

    $("#printStats").empty();
    $("#printStats").append("<tr><td>OG</td><td><strong>" + recipe.og.toFixed(3) + "</strong></td></tr>");
    $("#printStats").append("<tr><td>FG</td><td><strong>" + recipe.fg.toFixed(3) + "</strong></td></tr>");
    $("#printStats").append("<tr><td>IBU</td><td><strong>" + recipe.ibu.toFixed(0) + "</strong></td></tr>");
    $("#printStats").append("<tr><td>ABV</td><td><strong>" + recipe.abv.toFixed(1) + "%</strong></td></tr>");
    $("#printStats").append("<tr><td>Color</td><td><strong><span style='display: inline-block; width: 12px; height: 12px; background-color: " + Brauhaus.srmToCss(recipe.color) + "'></span> " + recipe.color.toFixed(0) + " SRM</strong></td></tr>");
    $("#printStats").append("<tr><td>Balance</td><td><strong>" + recipe.bv.toFixed(2) + "</strong></td></tr>");

    $('#preview').show();

    loadMaltTypeahead();
    loadHopTypeahead();
    loadYeastTypeahead();

    makeIngredientsSortable();

    fillBrewlog(recipe, measuredOG, measuredFG);
}

function makeIngredientsSortable() {
    Sortable.create(document.getElementById('fermentables'));
    Sortable.create(document.getElementById('hops'));
    Sortable.create(document.getElementById('yeasts'));
    Sortable.create(document.getElementById('extras'));
}

function loadMaltTypeahead() {
    var malts = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/search/autocompletemalt/%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('.malt.typeahead:not(.tt-input):not(.tt-hint)').typeahead(null, {
        name: 'maltSearch',
        source: malts,
        limit: 30,
        highlight: true
    });

    $('.malt.typeahead').on('typeahead:selected', function (e, searchTerm) {
        var id = $(this).closest("tr").attr("id");
        $.get("/malt/" + searchTerm + "/color", function (data) {
            $("#" + id + "-color").html(Brauhaus.srmToLovibond(Brauhaus.ebcToSrm(data)).toFixed(0) + "&deg; L");
        });
        $.get("/malt/" + searchTerm + "/yield", function (data) {
            $("#" + id + "-yield").html(data.toFixed(0) + "%");
        });
    });
}

function loadHopTypeahead() {
    $('.hop.typeahead:not(.tt-input):not(.tt-hint)').typeahead(null, {
        name: 'hopSearch',
        source: hops,
        limit: 30,
        highlight: true
    });

    $('.hop.typeahead').on('typeahead:selected', function (e, searchTerm) {
        var id = $(this).closest("tr").attr("id");
        $.get("/hop/" + searchTerm + "/alphaacid", function (data) {
            $("#" + id + "-aa").text(data + "%");
        });
    });
}

function loadYeastTypeahead() {
    var yeast = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/search/autocompleteyeast/%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('.yeast.typeahead:not(.tt-input):not(.tt-hint)').typeahead(null, {
        name: 'yeastSearch',
        source: yeast,
        limit: 30,
        highlight: true
    });

    $('.yeast.typeahead').on('typeahead:selected', function (e, searchTerm) {
        var id = $(this).closest("tr").attr("id");
        $.get("/yeast/" + searchTerm + "/attenuation", function (data) {
            $("#" + id + "-att").text(data + "%");
        });
        $.get("/yeast/" + searchTerm + "/lab", function (data) {
            $("#" + id + "-lab").text(data);
        });
    });
}

function saveRecipeToFirebase() {
    var id = $('#recipeId').val() === '' ? Math.floor(Math.random() * Number.MAX_SAFE_INTEGER) : parseInt($('#recipeId').val());
    var name = $('<div />').html($('#recipeName').val()).html();
    var beerxml = toBeerXml();
    var date = Date.parse($("#recipeDate").val());
    saveRecipe(id, name, date, beerxml);
}

function calculatePrimingSugar(v, vco2) {
    var t = 70;
    var gco2;

    var rco2 = 3.0378 - 5.0062e-2 * t + 2.6555e-4 * t * t;

    if ((gco2 = vco2 - rco2) <= 0) {
        return 0;
    }

    return Math.round(3.82 * v * gco2 * 100) / 100;
}


