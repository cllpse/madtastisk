using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Library;


namespace Iteration2.Controllers
{
    [LogError]
    public class OpskrifterController : Controller
    {
        [AlternateOutput]
        public ActionResult Find(String query)
        {
            if (query == null) return View();


            var matchedIngredients = Common.Auxiliary.MatchIngredients(query);

            IEnumerable<Models.RecipeDTO> matchedRecipes = new DataContext().Ingredients.WhereTrueForAny
            (
                a => b => a.Name == b.Name || (a.Name == b.Name && a.Amount >= b.Amount && a.Unit == b.Unit),
                matchedIngredients.ToArray()
            )
            .SelectMany(ir => ir.IngredientsRecipesRelations.Select(r => new Models.RecipeDTO
            {
                Id = r.RecipeId,
                Name = r.Recipe.Name,
                Description = r.Recipe.Description,
                Ingredients = new List<Models.IngredientDTO>(r.Recipe.IngredientsRecipesRelations.Select(ri => new Models.IngredientDTO
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Amount = ri.Ingredient.Amount,
                    Unit = ri.Ingredient.Unit,
                    MatchAccuracy = Common.Auxiliary.DetermineIngredientMatch(ri.Ingredient, matchedIngredients)
                }))
            }));

            var sortedRecipes = matchedRecipes.Distinct().OrderBy(i => i.Ingredients.Count(ma => ma.MatchAccuracy == Models.IngredientDTO.Accuracy.NoMatch));


            return View(sortedRecipes);
        }


        public ActionResult Vis(String query)
        {
            var sanitized = new Regex(Common.Auxiliary.REGEX_DASH).Replace(query, " ");

            
            if (sanitized == null) return View();

            
            return View(new DataContext().Recipes.Where(r => r.Name == sanitized).Select(r => new Models.RecipeDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Ingredients = new List<Models.IngredientDTO>(r.IngredientsRecipesRelations.Select(rir => new Models.IngredientDTO
                {
                    Id = rir.Ingredient.Id,
                    Name = rir.Ingredient.Name,
                    Amount = rir.Ingredient.Amount,
                    Unit = rir.Ingredient.Unit
                }))
            }).FirstOrDefault());
        }


        [AlternateOutput(JsonRequestBehavior.AllowGet)]
        public ActionResult Ingrediens(String query)
        {
            // ReSharper disable Asp.NotResolved
            return View(new DataContext().Ingredients.Where(i => i.Name.StartsWith(query)).Select(i => new Models.IngredientDTO { Name = i.Name }).Distinct());
            // ReSharper restore Asp.NotResolved
        }


        [AlternateOutput(JsonRequestBehavior.AllowGet)]
        public ActionResult IngrediensEnhed(String query)
        {
            // ReSharper disable Asp.NotResolved
            return View(new DataContext().Ingredients.Where(i => query == Common.Auxiliary.SUGGEST_QUERY_ALL_INGREDIENTS || i.Unit.StartsWith(query)).Select(i => new Models.IngredientDTO
            {
                Unit = i.Unit
            }).Distinct());
            // ReSharper restore Asp.NotResolved
        }


        [AlternateOutput(JsonRequestBehavior.AllowGet)]
        public ActionResult Navn(String query)
        {
            // ReSharper disable Asp.NotResolved
            return View(new DataContext().Recipes.Where(w => w.Name.StartsWith(query)).Select(r => new Models.RecipeDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).Distinct());
            // ReSharper restore Asp.NotResolved
        }


        [AlternateOutput(JsonRequestBehavior.AllowGet)]
        public ActionResult WildCard(String query)
        {
            return View(new DataContext().Recipes.Where(w => w.Name.IndexOf(query) > -1).Select(r => new Models.RecipeDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Ingredients = new List<Models.IngredientDTO>(r.IngredientsRecipesRelations.Select(rir => new Models.IngredientDTO
                {
                    Id = rir.Ingredient.Id,
                    Name = rir.Ingredient.Name,
                    Amount = rir.Ingredient.Amount,
                    Unit = rir.Ingredient.Unit
                }))
            }));
        }


        public ActionResult Del()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Del(FormCollection fc)
        {
            var dc = new DataContext();

            var recipe = new Entities.Recipe { Name = fc.Get("RecipeName"), Description = fc.Get("RecipePreperation") };

            var ingredients = Common.Auxiliary.MatchIngredients(fc.AllKeys.Where(k => k.StartsWith("addingredient")).Aggregate("", (s, key) => s + fc.Get(key) + "-")).ToList();

            ingredients = ingredients.Select(a => dc.Ingredients.Where(b => a.Name == b.Name && a.Amount == b.Amount && a.Unit == b.Unit).FirstOrDefault() ?? a).ToList();

            ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

            dc.Recipes.InsertOnSubmit(recipe);
            dc.SubmitChanges();


            return RedirectToAction("Vis", new { query = new Regex(Common.Auxiliary.REGEX_SPACE).Replace(recipe.Name, "-"), alternateOutputFormat = "html" });
        }


        public ActionResult ResetDatabase()
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Recipes.DeleteAllOnSubmit(dataContext.Recipes.Select(r => r));
                dataContext.IngredientsRecipesRelations.DeleteAllOnSubmit(dataContext.IngredientsRecipesRelations.Select(irr => irr));
                dataContext.Ingredients.DeleteAllOnSubmit(dataContext.Ingredients.Select(i => i));

                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Fantastiske fyldte tomater",
                    Description = @"Placer tomaterne med bunden i vejret. Skær 6 snit i hver tomat (sørg for ikke at skære helt igennem tomaterne! Stop snittene 1 cm fra bunden). 

Skær mozzarellaen i 6 tynde skiver pr. tomat. Hak de sorte oliven fint. Put en skive mozzarella ind i hver snit i tomaterne. 

Bland balsamico og olivenolie i en lille skål og smag til med salt og peber. Hæld balsamicoblandingen over tomaterne og drys med de hakkede oliven."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("bøftomater 2 stk, mozzarella 1 stk, oliven 1 stk, balsamico 1 spsk, olivenolie 2 spsk, salt 1 tsk, peber 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Krydret tomatsuppe",
                    Description = @"Purer tomaterne i en blender. 

Steg finthakket løg, hvidløg, jalapeno og ingefær i olivenolien i en stor gryde. Tilsæt kommen og steg videre under omrøring i et minuts tid. 

Tilsæt tomatpure, bouillon, sukker samt salt og lad det simre uden låg i ca. 20 min. Rør af og til. 

Blend suppen af 3-4 omgange (pas på når du blender varme væsker). Overfør suppen til en gryde og opvarm, hvis varm suppe ønskes. Kan også spises kold eller lunken."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("tomat 500 g, løg 1 stk, hvidløg 1 fed, jalapeno 1 tsk, ingefær 1 tsk, olivenolie 1 spsk, spidskommen 1 tsk, hønsebouillon 2 dl, sukker 1 tsk, salt 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Fyldte tomater",
                    Description = @"Kog risene. Skær et låg af tomaterne og udhul disse. Gem indmaden. 

Skær løg og peberfrugt i små tern og svits kort i en gryde - evt. med en smule olie. Indmaden fra tomaterne tilsættes sammen med basilikum hakket basilikum, salt og peber. Tilsæt til sidst ris og rør rundt mens blandingen kort koger op. 

Fordel blandingen i tomaterne og bag i ovnen i ca. 15-20 minutter ved 200 grader. 

Pynt med basilikumblade eller klippet purløg."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("ris 2 dl, tomat 4 stk, løg 1 stk, peberfrugt 1 stk, salt 1 tsk, peber 1 tsk, basilikum").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Tomatchutney",
                    Description = @"Prik tomaterne, skold dem og flå dem, og skær dem i skiver. 

Skær løg og chili i fine stykker. 

Varm det hele, inkl. krydderier, i en gryde under omrøring. 

Lad det småkoge i ca. 3-4 timer. 

Hæld det på efterfølgende på glas, evt. skyllede med atamon."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("tomat 1 kg, løg 125 g, chilipebber 3 stk, rørsukker 150 g, salt 1 spsk, karry 1 tsk, eddike 1 dl").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Lun tomatsalat",
                    Description = @"Læg tomat- og squashskiver i et ovnfast, fladt fad.

Fordel herpå skiveskårne løg, presset hvidløg, oliven og feta.

Dryp olivenolie på salaten og grill den øverst i ovnen, til osten er let gylden. Pynt med persille og server straks."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("tomat 4 stk, squash 1 stk, løg 1 stk, hvidløg 1 fed, oliven 50 g, feta 100 g, olivenolie 1 spsk, persille").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Tomatdressing",
                    Description = @"Kvark og tomatjuice røres sammen med friskhakket basilikum. Pres et fed hvidløg i dressingen. Smages til med salt og peber 

Serveres som tilbehør til blandet salat."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("kvark 50 g, tomatjuice 1 dl, basilikum 1 spsk, hvidløg 1 fed, salt 1 tsk, peber 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Hjemmelavet tomatsuppe",
                    Description = @"Opvarm olivenolie i en stor gryde over middel varme. Tilsæt hakket løg, oregano, timian samt hakket hvidløg. Steg i 5 min. under jævnlig omrøring. 

Tilsæt hakkede tomater samt resten af ingredienserne og rør godt rundt. Bring suppen i kog, skru ned for varmen og lad suppen simre i 15 min. 

Lad suppen afkøle lidt og hæld ca. halvdelen af suppen i en blender og purer den, til den er helt jævn (hvis den er for varm, eller der er for meget i blenderen, kan man risikere at låget på blenderen springer af) Overfør den purerede suppe til en skål. Gentag proceduren for resten af suppen. 

Server suppen varm eller afkølet. Pynt med hakket basilikum."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("olivenolie 1 tsk, løg 1 stk, oregano 1 tsk, timian 1 tsk, hvidløg 1 fed, tomat 2 stk, vand 1 dl, tomatpure 1 spsk, sukker 1 tsk, salt 1 tsk, peber 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Olivenbrød",
                    Description = @"Rør gæren ud i vandet, tilsæt resten og rør det godt sammen. Det bliver en meget våd dej. 

Lad den hæve til dobbelt størrelse et lunt sted. Dækkes over med et rent klæde. 

Pensl to bradepander med olivenolie og fordel dejen i bradepanden i et tyndt lag. 

Prik brødet tæt med en gaffel, skær det evt. ud i firkanter med en klejnespore, pensl med olivenolie, drys med groft salt og lad det hæve til dobbelt størrelse igen. Læg klædet over. 

Bag ved 250 grader i ca. 20 min. Tag brødet ud af ovnen og lad det køle af på en rist uden klæde."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("gær 25 g, vand 6 dl, yoghurt 2 dl, oliven 200 g, hvedemel 500 g, durummel 500 g, olivenolie 1 spsk, salt 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Penne med spinat, feta og oliven",
                    Description = @"Kog pastaen efter anvisning på emballagen. 

Dressing: Bland olivenolie, balsamico og knust hvidløg i en stor skål. 

Vend kogt pasta, hakket spinat, hakkede oliven, kapers og smuldret feta i dressingen. Server."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("pastapenne 350 g, olivenolie 2 spsk, balsamico 1 spsk, hvidløg 3 fed, spinat 75 g, oliven 100 g, kapers 2 spsk, feta 75g").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Pastasalat med peberfrugt",
                    Description = @"Kog pastaen efter anvisning på pakken. Skær peberfrugten i strimler, læg strimlerne i en si eller et dørslag og hæld kogevandet fra pastaen over peberfrugten. 

Skær cherrytomaterne i kvarte. Bland pasta, peberfrugtstrimler, tomater, hakket persille, salt, smuldret feta og skiveskårne oliven i en skål. Server salaten straks."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("pasta 175 g, peberfrugt 1 stk, cherrytomater 250 g, persille 2 dl, salt 1 tsk, feta 125 g, oliven 50 g").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Pastasalat med tun",
                    Description = @"Pastaen koges efter anvisning på posen og afkøles. Rødløg og tomater skæres i terninger. Pasta, ærter, løg og tomater blandes i en skål. Tunen findeles og tilsættes. 

Dressingen røres sammen og serveres ved siden af. 

Drys rigelig hakket persille over pastaen."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("pastaskruer 125 g, rødløg 1 stk, tomat 4 stk, ærter 100 g, tun 1 dåse, kvark 100 g, tomatjuice 1 dl, merian 1 spsk, salt 1 tsk, peber 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Frisk pasta",
                    Description = @"Hæld melet ud i en vulkan på køkkenbordet, lav et lille krater. Slå æggene ud heri sammen med den ekstra æggeblomme. 

Ælt massen med 1 tsk. olivenolie og groft salt efter behov til en ensartet masse. 

Pak dejen ind i husholdningsfilm og lad den hvile mindst en halv time i køleskab. 

Derefter rulles pastaen ud med pastamaskine."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("durummel 100 g, æg 3 stk, æggeblomme 1 stk, olivenolie 1 tsk, salt 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            using (var dataContext = new DataContext())
            {
                var recipe = new Entities.Recipe
                {
                    Name = "Fransk løgsuppe",
                    Description = @"Forvarm ovnen til 175 grader. Læg brødskiverne på en rist i ovnen og lad dem riste i ca. 5-10 min. til de er gyldne. 

Smør derefter brødskiverne med halvdelen af olivenolien på begge sider, og gnid med halverede hvidløg. Rist brødet endnu 5-10 min. 

Smelt samtidig smør og resten af olivenolien ved jævn varme og kom løg og salt i. Lad det snurre i ca. 20 min. eller til løgene begynder at blive brune.

Bring oksebouillonen i kog og hæld det over løgene, læg salvie i. Lad suppen koge til løgene er møre. Smag til med salt og peber. 

Ved servering lægges brødskiverne i terrin eller suppetallerken og suppen hældes over. Pynt til sidst med reven ost."
                };

                var ingredients = Common.Auxiliary.MatchIngredients("franskbrød 4 skiver, hvidløg 1 fed, oksekødsbouillon 2 l, løg 1 kg, olivenolie 2 spsk, salvie 4 blade, smør 4 spsk, salt 1 tsk, peber 1 tsk").ToList();

                ingredients.ForEach(i => recipe.IngredientsRecipesRelations.Add(new Entities.IngredientsRecipesRelation { Ingredient = i }));

                dataContext.Recipes.InsertOnSubmit(recipe);
                dataContext.SubmitChanges();
            }


            // ReSharper disable Asp.NotResolved
            return View();
            // ReSharper restore Asp.NotResolved
        }

        public ActionResult Alle()
        {
            return View(new DataContext().Recipes.Select(r => new Models.RecipeDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Ingredients = new List<Models.IngredientDTO>(r.IngredientsRecipesRelations.Select(rir => new Models.IngredientDTO
                {
                    Id = rir.Ingredient.Id,
                    Name = rir.Ingredient.Name,
                    Amount = rir.Ingredient.Amount,
                    Unit = rir.Ingredient.Unit
                }))
            }));
        }
    }
}