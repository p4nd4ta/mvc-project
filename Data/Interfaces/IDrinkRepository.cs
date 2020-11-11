using Drinks_Self_Learn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks_Self_Learn.Data.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> Drinks { get; } //get all Drinks
        IEnumerable<Drink> SearchDrinks(string searchTerm); //get all Drinks based on the search term provided
        IEnumerable<Drink> PreferredDrinks { get; } //get only the Preffered Drinks, that are shown on the homepage
        Drink GetDrinkById (int drinkId); //return drink object based on it's id

        // for more thorough look at the Design, please refer to the note I have left inside "Startup.cs", under the services configuration

        /* === INFORMATION ABOUT THE IMAGE SLIDES LOGIC ===
         * Slides Urls are held in the DB as a string seperated by a ';', which is URL reserved character.
         * Before the data is passed to the View it is "Deserialized" (using string.Split(';')) and passed to an array of type string(in the DrinkDetailsViewModel.cs) and then
         * the data is simply passed to the View and then using a loop, it iterates and auto generates the Carousel Slides, with the respective image URLs
         * 
         * === Admin Panel ===
         * In the Admin Panel, there are Create and Edit Actions,their working with the ImgUrlData is the following =>
         * simply get the URL data from DB and "Deserialize" it(using string.Split(';')) again, pass it to List in the VM.
         * And then in the View the list is iterated through, creating different textboxes, based on the items in the List.
         * An additional property called "UrlCounter" is declared in the ViewModel(value assigned in View, also an input type="hidden"), which "drives" the front-end logic,
         * used by the JavaScript(JQuery in SlidesValidaion.js), which gets the value of the current count and based on it creates, when the add(+) button is pressed,
         * a new dynamic input with name="UrlsArr[${index}]" and id="UrlsArr_${index}_", which on submit are passed to the list as new items,
         * the remove(-) button just removes the parrent of the textbox and decreases the counter with 1, which does't brake the logic at all.
         * Although on form submit the JS counter is assigned to UrlCounter value too, so there will be also additional checks based on that in the Controler, too.
         * On Edit/Create the ImageUrlData from the POST request gets "Serialized" with string.Join(';') from the list collection in the VM,
         * and persisted in DB in serialized format.
         */
    }
}
