using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();
            
            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Artist filterFind = Artists.Where(artist => artist.Hometown == "Mount Vernon").Single();
            Console.WriteLine("1. Artist from Mount Vernon");
            Console.WriteLine($"{filterFind.ArtistName} from Mt Vernon is {filterFind.Age}");

            //Who is the youngest artist in our collection of artists?
            Artist findyoung = Artists.OrderBy(artist =>  artist.Age).First();
            Console.WriteLine("2. Youngest artist");
            Console.WriteLine($"{findyoung.ArtistName} is the youngest artist, they are {findyoung.Age}.");

            //Display all artists with 'William' somewhere in their real name
            List<Artist> Williams = Artists.Where(artist => artist.RealName.Contains("William")).ToList();
            Console.WriteLine("3. Artists with 'William' somewhere in their real name");
            foreach(var artist in Williams) {
                Console.WriteLine($"{artist.ArtistName}'s real name is {artist.RealName}");
            }

            //Display the 3 oldest artist from Atlanta
            List<Artist> wiseBois = Artists.OrderByDescending(artist => artist.Age).Where(artist => artist.Hometown == "Atlanta").Take(3).ToList();
            Console.WriteLine("4. 3 oldests artist from Atlanta");
            foreach(var artist in wiseBois) {
                Console.WriteLine($"{artist.ArtistName} is {artist.Age}");
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            Console.WriteLine("5. Group Nmae of all groups that have members not from NYC");

            List<string> notNYCGroups = Artists.
                Join(Groups,
                    artist => artist.GroupId,
                    group => group.Id,
                    (artist, group) => {
                        artist.Group = group;
                        return artist;
                    })
                .Where(artist => (artist.Hometown != "New York City" && artist.Group != null))
                .Select(artist => artist.Group.GroupName)
                .Distinct()
                .ToList();

            foreach(var group in notNYCGroups) {
                Console.WriteLine(group);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
        }
    }
}
