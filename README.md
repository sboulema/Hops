# Hops
A website to help craft beer homebrewers

Hops has a large inventory of different kinds of hop. All information about the hop is gathered on a single easy page.
Most importantly every hop has a list of possible hop substitution.

Hops also has a large inventory of mals and yeasts and their respective information.

## Inventory
You can use Hops to keep track of your inventory of hops, malts and yeast. Just click the star icon to add an item to your inventory.

## Tools
Hops also has a few tools to make live easier for the homebrewer
- Bottle Priming Sugar 
- IBU
- Pitch Rate
- Label Printer

# Running Hops
Running Hops is easy and fast, just start the docker image and Hops will be served on port 5000

# BrewDB
Ingredient database is used from [BrewDB](https://github.com/sboulema/BrewDB)

## Environment variables

| Variable		 | Description								|
|----------------|------------------------------------------|
| firebaseApiKey | API key used to store inventory and recipes |
| YourlsApiKey	 | API key used to create short share urls for recipes |
