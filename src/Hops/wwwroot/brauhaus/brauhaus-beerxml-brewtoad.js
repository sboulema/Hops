﻿/*
@preserve
Brauhaus.js BeerXML Plugin
Copyright 2013 Daniel G. Taylor <danielgtaylor@gmail.com>
https://github.com/homebrewing/brauhausjs-beerxml
*/
! function () {
    var Brauhaus, DOMParser, _ref, _ref1;
    Brauhaus = (_ref = this.Brauhaus) != null ? _ref : require("brauhaus");
    DOMParser = (_ref1 = typeof window !== "undefined" && window !== null ? window.DOMParser : void 0) != null ? _ref1 : require("xmldom").DOMParser;
    Brauhaus.Recipe.fromBeerXml = function (xml) {
        var doc, fermentable, fermentableNode, fermentableProperty, mash, mashProperty, parser, recipe, recipeNode, recipeProperty, recipes, spice, spiceNode, spiceProperty, step, stepNode, stepProperty, styleNode, yeast, yeastNode, yeastProperty, _i, _j, _k, _l, _len, _len1, _len10, _len11, _len2, _len3, _len4, _len5, _len6, _len7, _len8, _len9, _m, _n, _o, _p, _q, _r, _ref10, _ref11, _ref12, _ref13, _ref14, _ref2, _ref3, _ref4, _ref5, _ref6, _ref7, _ref8, _ref9, _s, _t;
        recipes = [];
        parser = new DOMParser;
        doc = parser.parseFromString(xml, "text/xml");
        _ref2 = doc.documentElement.childNodes || [];
        for (_i = 0, _len = _ref2.length; _i < _len; _i++) {
            recipeNode = _ref2[_i];
            if (recipeNode.nodeName.toLowerCase() !== "recipe") {
                continue
            }
            recipe = new Brauhaus.Recipe;
            _ref3 = recipeNode.childNodes || [];
            for (_j = 0, _len1 = _ref3.length; _j < _len1; _j++) {
                recipeProperty = _ref3[_j];
                switch (recipeProperty.nodeName.toLowerCase()) {
                    case "name":
                        recipe.name = recipeProperty.textContent;
                        break;
                    case "brewer":
                        recipe.author = recipeProperty.textContent;
                        break;
                    case "batch_size":
                        recipe.batchSize = parseFloat(recipeProperty.textContent);
                        break;
                    case "boil_size":
                        recipe.boilSize = parseFloat(recipeProperty.textContent);
                        break;
                    case "boil_time":
                        recipe.boilTime = parseFloat(recipeProperty.textContent);
                        break;
                    case "type":
                        recipe.type = recipeProperty.textContent;
                        break;
                    case "date":
                        recipe.date = recipeProperty.textContent;
                        break;
                    case "og":
                        recipe.og = recipeProperty.textContent;
                        break;
                    case "fg":
                        recipe.fg = recipeProperty.textContent;
                        break;
                    case "taste_notes":
                        recipe.tasteNotes = recipeProperty.textContent;
                        break;
                    case "notes":
                        recipe.description = recipeProperty.textContent;
                        break;
                    case "taste_rating":
                        recipe.tasteRating = parseFloat(recipeProperty.textContent);
                        break;
                    case "efficiency":
                        recipe.mashEfficiency = parseFloat(recipeProperty.textContent);
                        break;
                    case "primary_age":
                        recipe.primaryDays = parseFloat(recipeProperty.textContent);
                        break;
                    case "primary_temp":
                        recipe.primaryTemp = parseFloat(recipeProperty.textContent);
                        break;
                    case "secondary_age":
                        recipe.secondaryDays = parseFloat(recipeProperty.textContent);
                        break;
                    case "secondary_temp":
                        recipe.secondaryTemp = parseFloat(recipeProperty.textContent);
                        break;
                    case "tertiary_age":
                        recipe.tertiaryDays = parseFloat(recipeProperty.textContent);
                        break;
                    case "tertiary_temp":
                        recipe.tertiaryTemp = parseFloat(recipeProperty.textContent);
                        break;
                    case "carbonation":
                        recipe.bottlingPressure = parseFloat(recipeProperty.textContent);
                        break;
                    case "carbonation_temp":
                        recipe.bottlingTemp = parseFloat(recipeProperty.textContent);
                        break;
                    case "forced_carbonation":
                        recipe.forcedCarbonation = recipeProperty.textContent;
                        break;
                    case "priming_sugar_name":
                        recipe.primingSugarName = recipeProperty.textContent;
                        break;
                    case "age":
                        recipe.agingDays = parseFloat(recipeProperty.textContent);
                        break;
                    case "age_temp":
                        recipe.agingTemp = parseFloat(recipeProperty.textContent);
                        break;
                    case "style":
                        recipe.style = {
                            og: [1, 1.15],
                            fg: [1, 1.15],
                            ibu: [0, 150],
                            color: [0, 500],
                            abv: [0, 14],
                            carb: [1, 4]
                        };
                        _ref4 = recipeProperty.childNodes || [];
                        for (_k = 0, _len2 = _ref4.length; _k < _len2; _k++) {
                            styleNode = _ref4[_k];
                            switch (styleNode.nodeName.toLowerCase()) {
                                case "name":
                                    recipe.style.name = styleNode.textContent;
                                    break;
                                case "category":
                                    recipe.style.category = styleNode.textContent;
                                    break;
                                case "og_min":
                                    recipe.style.og[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "og_max":
                                    recipe.style.og[1] = parseFloat(styleNode.textContent);
                                    break;
                                case "fg_min":
                                    recipe.style.fg[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "fg_max":
                                    recipe.style.fg[1] = parseFloat(styleNode.textContent);
                                    break;
                                case "ibu_min":
                                    recipe.style.ibu[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "ibu_max":
                                    recipe.style.ibu[1] = parseFloat(styleNode.textContent);
                                    break;
                                case "color_min":
                                    recipe.style.color[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "color_max":
                                    recipe.style.color[1] = parseFloat(styleNode.textContent);
                                    break;
                                case "abv_min":
                                    recipe.style.abv[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "abv_max":
                                    recipe.style.abv[1] = parseFloat(styleNode.textContent);
                                    break;
                                case "carb_min":
                                    recipe.style.carb[0] = parseFloat(styleNode.textContent);
                                    break;
                                case "carb_max":
                                    recipe.style.carb[1] = parseFloat(styleNode.textContent)
                            }
                        }
                        break;
                    case "fermentables":
                        _ref5 = recipeProperty.childNodes || [];
                        for (_l = 0, _len3 = _ref5.length; _l < _len3; _l++) {
                            fermentableNode = _ref5[_l];
                            if (fermentableNode.nodeName.toLowerCase() !== "fermentable") {
                                continue
                            }
                            fermentable = new Brauhaus.Fermentable;
                            _ref6 = fermentableNode.childNodes || [];
                            for (_m = 0, _len4 = _ref6.length; _m < _len4; _m++) {
                                fermentableProperty = _ref6[_m];
                                switch (fermentableProperty.nodeName.toLowerCase()) {
                                    case "name":
                                        fermentable.name = fermentableProperty.textContent;
                                        break;
                                    case "amount":
                                        fermentable.weight = parseFloat(fermentableProperty.textContent);
                                        break;
                                    case "yield":
                                        fermentable["yield"] = parseFloat(fermentableProperty.textContent);
                                        break;
                                    case "color":
                                        fermentable.color = parseFloat(fermentableProperty.textContent);
                                        break;
                                    case "add_after_boil":
                                        fermentable.late = fermentableProperty.textContent.toLowerCase() === "true"
                                }
                            }
                            recipe.fermentables.push(fermentable)
                        }
                        break;
                    case "hops":
                    case "miscs":
                        _ref7 = recipeProperty.childNodes || [];
                        for (_n = 0, _len5 = _ref7.length; _n < _len5; _n++) {
                            spiceNode = _ref7[_n];
                            if ((_ref8 = spiceNode.nodeName.toLowerCase()) !== "hop" && _ref8 !== "misc") {
                                continue
                            }
                            spice = new Brauhaus.Spice;
                            _ref9 = spiceNode.childNodes || [];
                            for (_o = 0, _len6 = _ref9.length; _o < _len6; _o++) {
                                spiceProperty = _ref9[_o];
                                switch (spiceProperty.nodeName.toLowerCase()) {
                                    case "name":
                                        spice.name = spiceProperty.textContent;
                                        break;
                                    case "amount":
                                        spice.weight = parseFloat(spiceProperty.textContent);
                                        break;
                                    case "alpha":
                                        spice.aa = parseFloat(spiceProperty.textContent);
                                        break;
                                    case "use":
                                        spice.use = spiceProperty.textContent;
                                        break;
                                    case "form":
                                        spice.form = spiceProperty.textContent;
                                        break;
                                    case "time":
                                        spice.time = spiceProperty.textContent;
                                        break;
                                    case "amount_is_weight":
                                        spice.amountIsWeight = (spiceProperty.textContent == 'true');
                                        break;
                                }
                            }
                            recipe.spices.push(spice)
                        }
                        break;
                    case "yeasts":
                        _ref10 = recipeProperty.childNodes || [];
                        for (_p = 0, _len7 = _ref10.length; _p < _len7; _p++) {
                            yeastNode = _ref10[_p];
                            if (yeastNode.nodeName.toLowerCase() !== "yeast") {
                                continue
                            }
                            yeast = new Brauhaus.Yeast;
                            _ref11 = yeastNode.childNodes || [];
                            for (_q = 0, _len8 = _ref11.length; _q < _len8; _q++) {
                                yeastProperty = _ref11[_q];
                                switch (yeastProperty.nodeName.toLowerCase()) {
                                    case "name":
                                        yeast.name = yeastProperty.textContent;
                                        break;
                                    case "type":
                                        yeast.type = yeastProperty.textContent;
                                        break;
                                    case "form":
                                        yeast.form = yeastProperty.textContent;
                                        break;
                                    case "attenuation":
                                        yeast.attenuation = parseFloat(yeastProperty.textContent);
                                        break;
                                    case "laboratory":
                                        yeast.laboratory = yeastProperty.textContent;
                                        break;
                                    case "amount":
                                        yeast.weight = parseFloat(yeastProperty.textContent);
                                        break;
                                    case "amount_is_weight":
                                        yeast.amountIsWeight = (yeastProperty.textContent == 'true');
                                        break;
                                }
                            }
                            recipe.yeast.push(yeast)
                        }
                        break;
                    case "mash":
                        mash = recipe.mash = new Brauhaus.Mash;
                        _ref12 = recipeProperty.childNodes || [];
                        for (_r = 0, _len9 = _ref12.length; _r < _len9; _r++) {
                            mashProperty = _ref12[_r];
                            switch (mashProperty.nodeName.toLowerCase()) {
                                case "name":
                                    mash.name = mashProperty.textContent;
                                    break;
                                case "grain_temp":
                                    mash.grainTemp = parseFloat(mashProperty.textContent);
                                    break;
                                case "sparge_temp":
                                    mash.spargeTemp = parseFloat(mashProperty.textContent);
                                    break;
                                case "ph":
                                    mash.ph = parseFloat(mashProperty.textContent);
                                    break;
                                case "notes":
                                    mash.notes = mashProperty.textContent;
                                    break;
                                case "mash_steps":
                                    _ref13 = mashProperty.childNodes || [];
                                    for (_s = 0, _len10 = _ref13.length; _s < _len10; _s++) {
                                        stepNode = _ref13[_s];
                                        if (stepNode.nodeName.toLowerCase() !== "mash_step") {
                                            continue
                                        }
                                        step = new Brauhaus.MashStep;
                                        _ref14 = stepNode.childNodes || [];
                                        for (_t = 0, _len11 = _ref14.length; _t < _len11; _t++) {
                                            stepProperty = _ref14[_t];
                                            switch (stepProperty.nodeName.toLowerCase()) {
                                                case "name":
                                                    step.name = stepProperty.textContent;
                                                    break;
                                                case "type":
                                                    step.type = stepProperty.textContent;
                                                    break;
                                                case "infuse_amount":
                                                    step.waterRatio = parseFloat(stepProperty.textContent) / recipe.grainWeight();
                                                    break;
                                                case "step_temp":
                                                    step.temp = parseFloat(stepProperty.textContent);
                                                    break;
                                                case "end_temp":
                                                    step.endTemp = parseFloat(stepProperty.textContent);
                                                    break;
                                                case "step_time":
                                                    step.time = parseFloat(stepProperty.textContent);
                                                    break;
                                                case "decoction_amt":
                                                    step.waterRatio = parseFloat(stepProperty.textContent) / recipe.grainWeight()
                                            }
                                        }
                                        mash.steps.push(step)
                                    }
                            }
                        }
                }
            }
            recipes.push(recipe)
        }
        return recipes
    };
    Brauhaus.Recipe.prototype.toBeerXml = function () {
        var fermentable, hop, misc, step, xml, yeast, _i, _j, _k, _l, _len, _len1, _len2, _len3, _len4, _m, _ref2, _ref3, _ref4, _ref5, _ref6;
        xml = '<?xml version="1.0" encoding="utf-8"?><recipes><recipe>';
        xml += "<version>1</version>";
        xml += "<name>" + this.name + "</name>";
        xml += "<brewer>" + this.author + "</brewer>";
        xml += "<batch_size>" + this.batchSize + "</batch_size>";
        xml += "<boil_size>" + this.boilSize + "</boil_size>";
        xml += "<boil_time>" + this.boilTime + "</boil_time>";
        xml += "<efficiency>" + this.mashEfficiency + "</efficiency>";
        xml += "<type>" + this.type + "</type>";
        xml += "<date>" + this.date + "</date>";
        xml += "<notes>" + this.description + "</notes>";
        if (this.primaryDays) {
            xml += "<primary_age>" + this.primaryDays + "</primary_age>"
        }
        if (this.primaryTemp) {
            xml += "<primary_temp>" + this.primaryTemp + "</primary_temp>"
        }
        if (this.secondaryDays) {
            xml += "<secondary_age>" + this.secondaryDays + "</secondary_age>"
        }
        if (this.secondaryTemp) {
            xml += "<secondary_temp>" + this.secondaryTemp + "</secondary_temp>"
        }
        if (this.tertiaryDays) {
            xml += "<tertiary_age>" + this.tertiaryDays + "</tertiary_age>"
        }
        if (this.tertiaryTemp) {
            xml += "<tertiary_temp>" + this.tertiaryTemp + "</tertiary_temp>"
        }
        if (this.agingDays) {
            xml += "<age>" + this.agingDays + "</age>"
        }
        if (this.agingTemp) {
            xml += "<age_temp>" + this.agingTemp + "</age_temp>"
        }
        if (this.bottlingTemp) {
            xml += "<carbonation_temp>" + this.bottlingTemp + "</carbonation_temp>"
        }
        if (this.bottlingPressure) {
            xml += "<carbonation>" + this.bottlingPressure + "</carbonation>"
        }
        if (this.forcedCarbonation) {
            xml += "<forced_carbonation>" + this.forcedCarbonation + "</forced_carbonation>"
        }
        if (this.primingSugarName) {
            xml += "<priming_sugar_name>" + this.primingSugarName + "</priming_sugar_name>"
        }
        if (this.og) {
            xml += "<og>" + this.og + "</og>"
        }
        if (this.fg) {
            xml += "<fg>" + this.fg + "</fg>"
        }
        if (this.tasteNotes) {
            xml += "<taste_notes>" + this.tasteNotes + "</taste_notes>"
        }
        if (this.tasteRating) {
            xml += "<taste_rating>" + this.tasteRating + "</taste_rating>"
        }
        if (this.style) {
            xml += "<style><version>1</version>";
            if (this.style.name) {
                xml += "<name>" + this.style.name + "</name>"
            }
            if (this.style.category) {
                xml += "<category>" + this.style.category + "</category>"
            }
            xml += "<og_min>" + this.style.og[0] + "</og_min><og_max>" + this.style.og[1] + "</og_max>";
            xml += "<fg_min>" + this.style.fg[0] + "</fg_min><fg_max>" + this.style.fg[1] + "</fg_max>";
            xml += "<ibu_min>" + this.style.ibu[0] + "</ibu_min><ibu_max>" + this.style.ibu[1] + "</ibu_max>";
            xml += "<color_min>" + this.style.color[0] + "</color_min><color_max>" + this.style.color[1] + "</color_max>";
            xml += "<abv_min>" + this.style.abv[0] + "</abv_min><abv_max>" + this.style.abv[1] + "</abv_max>";
            xml += "<carb_min>" + this.style.carb[0] + "</carb_min><carb_max>" + this.style.carb[1] + "</carb_max>";
            xml += "</style>"
        }
        xml += "<fermentables>";
        _ref2 = this.fermentables;
        for (_i = 0, _len = _ref2.length; _i < _len; _i++) {
            fermentable = _ref2[_i];
            xml += "<fermentable><version>1</version>";
            xml += "<name>" + fermentable.name + "</name>";
            xml += "<type>" + fermentable.type() + "</type>";
            xml += "<amount>" + fermentable.weight.toFixed(3) + "</amount>";
            xml += "<yield>" + fermentable["yield"].toFixed(1) + "</yield>";
            xml += "<color>" + fermentable.color.toFixed(1) + "</color>";
            xml += "</fermentable>"
        }
        xml += "</fermentables>";
        xml += "<hops>";
        _ref3 = this.spices.filter(function (item) {
            return item.aa > 0
        });
        for (_j = 0, _len1 = _ref3.length; _j < _len1; _j++) {
            hop = _ref3[_j];
            xml += "<hop><version>1</version>";
            xml += "<name>" + hop.name + "</name>";
            xml += "<time>" + hop.time + "</time>";
            xml += "<amount>" + hop.weight + "</amount>";
            xml += "<alpha>" + hop.aa.toFixed(2) + "</alpha>";
            xml += "<use>" + hop.use + "</use>";
            xml += "<form>" + hop.form + "</form>";
            xml += "</hop>"
        }
        xml += "</hops>";
        xml += "<yeasts>";
        _ref4 = this.yeast;
        for (_k = 0, _len2 = _ref4.length; _k < _len2; _k++) {
            yeast = _ref4[_k];
            xml += "<yeast><version>1</version>";
            xml += "<name>" + yeast.name + "</name>";
            xml += "<type>" + yeast.type + "</type>";
            xml += "<form>" + yeast.form + "</form>";
            xml += "<attenuation>" + yeast.attenuation + "</attenuation>";
            xml += "<amount>" + yeast.weight + "</amount>";
            xml += "<laboratory>" + yeast.laboratory + "</laboratory>";
            xml += "</yeast>"
        }
        xml += "</yeasts>";
        xml += "<miscs>";
        _ref5 = this.spices.filter(function (item) {
            return item.aa === 0
        });
        for (_l = 0, _len3 = _ref5.length; _l < _len3; _l++) {
            misc = _ref5[_l];
            xml += "<misc><version>1</version>";
            xml += "<name>" + misc.name + "</name>";
            xml += "<time>" + misc.time + "</time>";
            xml += "<amount>" + misc.weight + "</amount>";
            xml += "<use>" + misc.use + "</use>";
            xml += "</misc>"
        }
        xml += "</miscs>";
        if (this.mash) {
            xml += "<mash><version>1</version>";
            xml += "<name>" + this.mash.name + "</name>";
            xml += "<grain_temp>" + this.mash.grainTemp + "</grain_temp>";
            xml += "<sparge_temp>" + this.mash.spargeTemp + "</sparge_temp>";
            xml += "<ph>" + this.mash.ph + "</ph>";
            xml += "<notes>" + this.mash.notes + "</notes>";
            xml += "<mash_steps>";
            _ref6 = this.mash.steps;
            for (_m = 0, _len4 = _ref6.length; _m < _len4; _m++) {
                step = _ref6[_m];
                xml += "<mash_step><version>1</version>";
                xml += "<name>" + step.name + "</name>";
                xml += "<description>" + step.description(true, this.grainWeight()) + "</description>";
                xml += "<step_time>" + step.time + "</step_time>";
                xml += "<step_temp>" + step.temp + "</step_temp>";
                xml += "<end_temp>" + step.endTemp + "</end_temp>";
                xml += "<ramp_time>" + step.rampTime + "</ramp_time>";
                if (step.type === "Decoction") {
                    xml += "<decoction_amt>" + step.waterRatio * this.grainWeight() + "</decoction_amt>"
                } else {
                    xml += "<infuse_amount>" + step.waterRatio * this.grainWeight() + "</infuse_amount>"
                }
                xml += "</mash_step>"
            }
            xml += "</mash_steps>";
            xml += "</mash>"
        }
        return xml += "</recipe></recipes>"
    }
}.call(this);