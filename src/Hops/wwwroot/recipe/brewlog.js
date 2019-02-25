function fillBrewlog(recipe, measuredOG, measuredFG) {
    if (typeof recipe.date !== 'undefined') {
        $("#recipeDate").val(new Date(parseInt(recipe.date)).toISOString().substring(0, 10));
    } else {
        $("#recipeDate").val(new Date(parseInt(Date.now())).toISOString().substring(0, 10))
    }

    $("#recipeOG").val(parseFloat(measuredOG).toFixed(3));
    $("#predictedOG").text(parseFloat(recipe.og).toFixed(3) + " predicted");

    $("#recipeFG").val(parseFloat(measuredFG).toFixed(3));
    $("#predictedFG").text(parseFloat(recipe.fg).toFixed(3) + " predicted");

    var measuredABV = (measuredOG - measuredFG) * 131;
    $("#measuredABV").text(measuredABV.toFixed(1) + '%');

    $("#recipeTastingNotes").val(recipe.tasteNotes);
    $("#recipeRating").val(parseFloat(recipe.tasteRating) / 10);

    $("#carbonationLevel").val(recipe.bottlingPressure);
    $("#forcedCarbonation").prop('checked', recipe.forcedCarbonation);
    $("#primingSugarName").val(recipe.primingSugarName);
    $("#neededPrimingSugar").text(calculatePrimingSugar(parseFloat(recipe.batchSize), parseFloat($("#carbonationLevel").val())) + "g sugar needed");
}

$(document).ready(function () {
    $("#recipeDate").val(new Date(parseInt(Date.now())).toISOString().substring(0, 10));

    $("#recipeOG").change(function () {
        var measuredABV = (measuredOG - measuredFG) * 131;
        $("#measuredABV").text(measuredABV.toFixed(1) + '%');
    });

    $("#recipeFG").change(function () {
        var measuredABV = (measuredOG - measuredFG) * 131;
        $("#measuredABV").text(measuredABV.toFixed(1) + '%');
    });

    $("#carbonationLevel").change(function () {
        $("#neededPrimingSugar").text(calculatePrimingSugar(parseFloat(recipe.batchSize), parseFloat($("#carbonationLevel").val())) + "g sugar needed");
    });
});