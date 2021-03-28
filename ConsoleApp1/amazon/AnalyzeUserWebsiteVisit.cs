using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace ConsoleApp1.Amazon
{
    public class AnalyzeUserWebsiteVisit 
    {
        public static void Test()
        {
            new AnalyzeUserWebsiteVisit().MostVisitedPattern(
                new[]{"joe","joe","joe","james","james","james","james","mary","mary","mary"},
                new[] {1,2,3,4,5,6,7,8,9,10},
                new[]{"home","about","career","home","cart","maps","home","home","about","career"}
            ).Should().Equal(new[]{"home","about","career"});

             new AnalyzeUserWebsiteVisit().MostVisitedPattern(
                new[]{"u1","u1","u1","u2","u2","u2"},
                new[] {1,2,3,4,5,6},
                new[]{"a","b","a","a","b","c"}
            ).Should().Equal(new[]{"a","b","a"});

             new AnalyzeUserWebsiteVisit().MostVisitedPattern(
                new[]{"h","eiy","cq","h","cq","txldsscx","cq","txldsscx","h","cq","cq"},
                new[] {527896567,334462937,517687281,134127993,859112386,159548699,51100299,444082139,926837079,317455832,411747930},
                new[]{"hibympufi","hibympufi","hibympufi","hibympufi","hibympufi","hibympufi","hibympufi","hibympufi","yljmntrclw","hibympufi","yljmntrclw"}
            ).Should().Equal(new[]{"hibympufi","hibympufi","yljmntrclw"});

             new AnalyzeUserWebsiteVisit().MostVisitedPattern(
                new[]{"zkiikgv","zkiikgv","zkiikgv","zkiikgv"},
                new[] {436363475,710406388,386655081,797150921},
                new[]{"wnaaxbfhxp","mryxsjc","oz","wlarkzzqht"}
            ).Should().Equal(new[]{"oz","mryxsjc","wlarkzzqht"});

             new AnalyzeUserWebsiteVisit().MostVisitedPattern(
                new[]{"him","mxcmo","jejuvvtye","wphmqzn","uwlblbrkqv","flntc","esdtyvfs","nig","jejuvvtye","nig","mxcmo","flntc","nig","jejuvvtye","odmspeq","jiufvjy","esdtyvfs","mfieoxff","nig","flntc","mxcmo","qxbrmo"},
                new[] {113355592,304993712,80831183,751306572,34485202,414560488,667775008,951168362,794457022,813255204,922111713,127547164,906590066,685654550,430221607,699844334,358754380,301537469,561750506,612256123,396990840,60109482},
                new[]{"k","o","o","nxpvmh","dssdnkv","kiuorlwdcw","twwginujc","evenodb","qqlw","mhpzoaiw","jukowcnnaz","m","ep","qn","wxeffbcy","ggwzd","tawp","gxm","pop","xipfkhac","weiujzjcy","x"}
            ).Should().Equal(new[]{"m","kiuorlwdcw","xipfkhac"});     

            Console.WriteLine("MostVisitedPattern is done!");       
        }

        public IList<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website) 
        {                                    
            if((timestamp?.Length ?? 0) < 3)
                return null;

            // sort sessions by timestamp O(NLogN)
            IEnumerable<(int t, string user, string website)> sessions = timestamp.Select((t, i) => (t, username[i], website[i])).OrderBy(s => s.t);

            // group websites by username O(N)
            Dictionary<string, List<string>> userWebsites = new Dictionary<string, List<string>>();
            foreach(var s in sessions)
            {
                if(!userWebsites.ContainsKey(s.user))
                    userWebsites.Add(s.user, new List<string>());
                userWebsites[s.user].Add(s.website);
            }

            // get 3-seqs freq
            Dictionary<(string, string, string), int> freq3Seqs = new Dictionary<(string, string, string), int>();
            int maxVisit = 0;
            string[] maxSequince = new string[3];
            foreach(var uws in userWebsites.Values.Where(l => l.Count > 2))
            {
                var user3Seqs = All3Sequences(uws);
                foreach(var u3s in user3Seqs)
                {
                    var key = (u3s[0], u3s[1], u3s[2]);                    
                    if(!freq3Seqs.ContainsKey(key))
                        freq3Seqs.Add(key, 0);
                    freq3Seqs[key]++;
                    
                    if(freq3Seqs[key] > maxVisit || (freq3Seqs[key] == maxVisit && LexiCompare(u3s, maxSequince) < 0))
                    {
                        maxSequince = u3s;
                        maxVisit = freq3Seqs[key];
                    }                    
                }
            }
            
            return maxSequince.ToList();
        }

        private int LexiCompare(string[] x, string[] y)
        {
        int l = Math.Min(x.Length, y.Length);
        for(int i = 1; i < l; i++)
        {
            var r = StringComparer.OrdinalIgnoreCase.Compare(x[i], y[i]);
            if(r != 0)
            return r;
        }
        if(x.Length == y.Length)
            return StringComparer.OrdinalIgnoreCase.Compare(x[0], y[0]);
        else if(x.Length < y.Length)
            return -1;
        else
            return 1;
        }
        
        private IEnumerable<string[]> All3Sequences(IList<string> s)
        {            
            int start = 0;
            int end = (s?.Count ?? 0) - 1;
            if(end - start < 2)
                yield break;

            HashSet<(string, string, string)> uniq = new HashSet<(string, string, string)>();            
            for(int i = start; i<= end - 2; i++)
            for(int j = i + 1; j <= end - 1; j++)
            for(int k = j + 1; k <= end; k++)
            {                
                var key = (s[i], s[j], s[k]);
                if(!uniq.Contains(key))
                {
                    uniq.Add(key);
                    yield return new[]{s[i], s[j], s[k]};
                }
            }

        }
    }
}