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

    /* https://leetcode.com/discuss/interview-question/587819/amazon-interview-question-amazon-kindle-top-features
    Abstract
    You work on the Kindle team and are trying to understand which features users want to added to a new version of the Kindle the most. Your team has received a large number of feature requests from users.

    Write an algorithm that identifies the most popular N feature requests out of a list of feature requests and possible features. Your algorithm should output the most frequently mentioned features.

    Input
    The input to the function/method consists of five arguments -
    numFeatures, an integer representing the number of possible features;
    topFeatures, an integer representing the number of top features that your function returns (N);
    possibleFeatures, a list of sigle-word strings representing the possible features;
    numFeatureRequests, an integer representing the number of feature requests;
    featureRequests, a list of strings where each element is a string that consists of space-separeted words representing feature requests.

    Output
    Return a list of strings representing the most popular N feature requests in order of most to least frequently mentioned.

    Note
    The comparison of strings is case-insensitive.
    If the value topFeatures is more that the number of possible features, return the names of only the features mentioned in the feature requests.
    Multiple ocurrence of a feature in a quote should be considered as a single mention.
    If features are mentioned an equal number of times in feature request (e.g. newshop = 2, shopnow = 2, mymarket = 4), sort alphabetically, topFeatures = 2, Output = [mymarket, newshop]

    Example
    Input:
    numFeatures = 6
    topFeatures = 2
    possibleFeatures = ["storage", "battery", "hover", "alexa", "waterproof", "solar"]
    numFeatureRequests = 7
    featureRequests = ["I wish my Kindle had even more storage",
    "I wish the battery life on my Kindle lasted 2 years", "I read in the bath and would enjoy a waterproof Kindle",
    "I want to take my Kindle into the hover. Waterproof please waterproof!", "It would be neat if my Kindle would hover on my desk when not in use",
    "How cool would it be if my Kindle charged in the sun via solar power?"]

    Output
    ["waterproof", "battery"]

    Explanation:
    "waterproof" occurs in three different requests and "battery" in two. "hover", "solar", and "storage" occur in only one request each.
    */
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