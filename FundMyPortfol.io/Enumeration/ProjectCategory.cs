using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FundMyPortfol.io.Enumeration
{
    public class ProjectCategory {
        public enum Category
        {
            [Description("innovation")] INNOVATION,
            [Description("arts")] ARTS,
            [Description("crafts")] CRAFTS,
            [Description("comics")] COMICS,
            [Description("film")] FILM,
            [Description("food")] FOOD,
            [Description("gadget")] GADGET,
            [Description("games")] GAMES,
            [Description("music")] MUSIC,
            [Description("publishing")] PUBLISHING,
            [Description("software")] SOFTWARE
        }
        public string ToString(Category category)
        {
            switch (category)
            {
                case Category.ARTS:
                    return "arts";
                case Category.CRAFTS:
                    return "crafts";
                case Category.COMICS:
                    return "comics";
                case Category.FILM:
                    return "film";
                case Category.FOOD:
                    return "food";
                case Category.GADGET:
                    return "gadget";
                case Category.GAMES:
                    return "games";
                case Category.MUSIC:
                    return "music";
                case Category.PUBLISHING:
                    return "publishing";
                case Category.SOFTWARE:
                    return "software";
                default:
                    return "innovation";
            }
        }

        public Category ToCategory(string str)
        {
            switch (str)
            {
                case "arts":
                    return Category.ARTS;
                case "crafts":
                    return Category.CRAFTS;
                case "comics":
                    return Category.COMICS;
                case "film":
                    return Category.FILM;
                case "food":
                    return Category.FOOD;
                case "gadget":
                    return Category.GADGET;
                case "games":
                    return Category.GAMES;
                case "music":
                    return Category.MUSIC;
                case "publishing":
                    return Category.PUBLISHING;
                case "software":
                    return Category.SOFTWARE;
                default:
                    return Category.INNOVATION;
            }
        }
    }     
}