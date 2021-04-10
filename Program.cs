using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AnagramSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dictionaryHash = GetDictionaryHash(); 

            var sortedAnagramString = GetSortedAnagram();

            var validAnagram = !String.IsNullOrEmpty(sortedAnagramString);
            var hasSolution = dictionaryHash.ContainsKey(sortedAnagramString);
            if (validAnagram && hasSolution)
            {
                GetResults(dictionaryHash, sortedAnagramString);
            }
            else
            {
                Console.WriteLine(!validAnagram ? "\nInput can't be null or empty\n" : "\nNo solution found\n");
            }
        }

        /// <summary>
        /// Writes solutions to the given anagram in the console, if there is any
        /// </summary>
        private static void GetResults(Dictionary<string, List<string>> dictionaryHash, 
            string sortedAnagramString)
        {
            var results = dictionaryHash[sortedAnagramString];

                Console.WriteLine("\n\n Solutions: \n");
                
                results.ForEach(sol => {
                    Console.WriteLine($"> {sol}");
                });

                Console.WriteLine("\n");
        }


        /// <summary>
        /// Returns user console input as a string
        /// </summary>
        /// <return>
        /// A string representing the input anagram in sorted order
        /// </returns>
        private static string GetSortedAnagram()
        {
            Console.WriteLine("\nType your anagram here and press ENTER\n");
            var anagram = Console.ReadLine().ToLower();
            var sortedAnagramString = String.Concat(anagram.OrderBy(c => c));

            return sortedAnagramString;
        }

        /// <summary>
        /// Returns a dictionary of words.
        /// </summary>
        /// <returns>
        /// Returns a Dictionary<string, List<string>> where the key is a word with its characters
        /// in sorted order and its value is a list of strings representing all of the words
        /// that can be made from the key.
        /// </returns>
        private static Dictionary<string, List<string>> GetDictionaryHash()
        {
            string jsonString;

            try  
            {  
                using (StreamReader reader = new StreamReader("dictionary.json"))  
                {  
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (Exception ex)  
            {  
               throw new Exception($"Failed to read dictionary.json: {ex.Message}");  
            }   
          
            var dictionaryHash = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonString);

            return dictionaryHash;
        }
    }
}
