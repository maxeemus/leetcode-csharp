using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp1.Common;

namespace ConsoleApp1.Amazon
{
    // IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
    // SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
    // DEFINE ANY CLASS AND METHOD NEEDED
    // CLASS BEGINS, THIS CLASS IS REQUIRED
    public class KMostRequired
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public List<string> popularNFeatures(int numFeatures,
                                            int topFeatures,
                                            List<string> possibleFeatures,
                                            int numFeatureRequests,
                                            List<string> featureRequests)
        {            
            if(topFeatures <= 0)
                return new List<string>(0);
            if(possibleFeatures == null || !possibleFeatures.Any())
                return new List<string>(0);
            if(featureRequests == null || !featureRequests.Any())
                return new List<string>(0);
                    
            // set of possible features
            var possibleFeaturesSet = new HashSet<string>(possibleFeatures, StringComparer.CurrentCultureIgnoreCase);
            // map of feature Request words and freq
            var featureRequestsFreq = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            foreach(var featureReq in featureRequests)
            {
                // Exctract words from featureRequests                 
                var featureRequestWords = rgx
                    .Replace(featureReq, "") // replace non-alphanumeric
                    .Split() //  split words
                    .Where(w => possibleFeaturesSet.Contains(w)) // leave only possible Features                    
                    .Distinct(StringComparer.CurrentCultureIgnoreCase); // multiple occurence consider as single

                // Calculate freq.
                foreach(var frWord in featureRequestWords)
                {
                    if(!featureRequestsFreq.ContainsKey(frWord))
                        featureRequestsFreq.Add(frWord, 0);
                    featureRequestsFreq[frWord]++;
                }
            }            

            // Min priority heap
            // priority is max freq. + min alphabetically            
            var pq = new MinHeap<(string feauture, int freq)>(Comparer<(string feauture, int freq)>.Create((x, y) => 
            {
                var c =  x.freq - y.freq; 
                if(c == 0) // the same freq - compare words
                     return StringComparer.CurrentCultureIgnoreCase.Compare(y.feauture, x.feauture);
                else 
                    return c;
            }));

            // pq will have topFeatures max. freq. features
            foreach(var frfPair in featureRequestsFreq)
            {
                pq.Enqueue((frfPair.Key, frfPair.Value));
                if(pq.Count > topFeatures) 
                     pq.Dequeue(); // remove min element
            }
            
            var result = new string[pq.Count];
            int i = result.Length - 1;
            while(pq.Count > 0)
            {
                var d = pq.Dequeue();
                result[i--] = d.feauture;
            }
            return result.ToList();
        }
        // METHOD SIGNATURE ENDS
    }    
}