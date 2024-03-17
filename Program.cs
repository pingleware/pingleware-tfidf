using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace pingleware_tfidf
{
    class Program
    {
        static List<string> wordsWithoutStopwords = new List<string>();
        static List<string> uniqueWords = new List<string>();
        static int wordCountSentences = 0;
        static int wordCountSentencesLength = 0;

        static string[] Stopwords = {"a", "share", "linkthese", "about", "above", "after", "again", "against", "all", "am", "an", "and", "any", "", "are", "aren't", "as", "at", "be", "because", "been", "before", "being", "below", "between", "both", "but", "by", "can't", "cannot", "could", "couldn't", "did", "didn't", "do", "does", "doesn't", "doing", "don't", "down", "during", "each", "few", "for", "from", "further", "had", "hadn't", "has", "hasn't", "have", "haven't", "having", "he", "he'd", "he'll", "he's", "her", "here", "here's", "hers", "herself", "him", "himself", "his", "how", "how's", "i", "i'd", "i'll", "i'm", "i've", "if", "in", "into", "is", "isn't", "it", "it's", "its", "itself", "let's", "me", "more", "most", "mustn't", "my", "myself", "no", "nor", "not", "of", "off", "on", "once", "only", "or", "other", "ought", "our", "ours", "ourselves", "out", "over", "own", "same", "shan't", "she", "she'd", "she'll", "she's", "should", "shouldn't", "so", "some", "such", "than", "that", "that's", "the", "their", "theirs", "them", "themselves", "then", "there", "there's", "these", "they", "they'd", "they'll", "they're", "they've", "this", "those", "through", "to", "too", "under", "until", "up", "very", "was", "wasn't", "we", "we'd", "we'll", "we're", "we've", "were", "weren't", "what", "what's", "when", "when's", "where", "where's", "which", "while", "who", "who's", "whom", "why", "why's", "with", "won't", "would", "wouldn't", "you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves", "this", "messenger", "facebook"};

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide input text as command-line argument.");
                return;
            }

            string inputText = File.ReadAllText(args[0]);
            var result = TFIDF(inputText);
            Console.WriteLine("Top 3 sentences with highest TF*IDF:");
            foreach (var sentence in result)
            {
                Console.WriteLine(sentence);
            }
        }

        static List<string> Prettify(string documents)
        {
            var documentInLowercase = documents.Replace(".", "").Split(" ").Select(x => x.ToLower()).ToList();
            return documentInLowercase.Where(x => !Stopwords.Contains(x)).ToList();
        }

        static List<string> UniqueWords(List<string> words)
        {
            return words.Distinct().ToList();
        }

        static Dictionary<string, int> CountWords(List<string> words)
        {
            var uniqueWords = UniqueWords(words);
            var dict = new Dictionary<string, int>();
            foreach (var word in uniqueWords)
            {
                dict[word] = 0;
                foreach (var w in wordsWithoutStopwords)
                {
                    if (word == w)
                    {
                        dict[word]++;
                    }
                }
            }
            return dict;
        }

        static Dictionary<string, double> TermFrequency(string documents)
        {
            wordsWithoutStopwords = Prettify(documents);
            var sentences = documents.Split('.').Select(item => item.Trim()).ToArray();
            // ISSUE:  System.ArgumentOutOfRangeException: startIndex cannot be larger than length of string. (Parameter 'startIndex')
            //sentences[0] = sentences[0].Substring(146);
            var TFVals = CountWords(wordsWithoutStopwords);
            var uniqueWords = UniqueWords(wordsWithoutStopwords);
            foreach (var key in TFVals.Keys)
            {
                TFVals[key] = TFVals[key] / wordsWithoutStopwords.Count;
            }
            var TFSentences = new Dictionary<string, double>();
            foreach (var sentence in sentences)
            {
                var sentenceSplitWords = sentence.Split(" ");
                var tempAdd = 0.0;
                var wordsNoStopWordsLength = Prettify(sentence).Count;
                foreach (var word in sentenceSplitWords)
                {
                    if (TFVals.ContainsKey(word.ToLower()))
                    {
                        tempAdd += TFVals[word.ToLower()];
                    }
                }
                TFSentences[sentence] = tempAdd / wordsNoStopWordsLength;
            }
            return TFSentences;
        }

        static Dictionary<string, double> InverseDocumentFrequency(string documents)
        {
            var wordsWithoutStopwords = Prettify(documents);
            var uniqueWordsSet = UniqueWords(wordsWithoutStopwords);
            var sentences = documents.Split('.').Select(item => item.Trim()).ToArray();
            // ISSUE:  System.ArgumentOutOfRangeException: startIndex cannot be larger than length of string. (Parameter 'startIndex')
            //sentences[0] = sentences[0].Substring(146);
            var lengthOfDocuments = sentences.Length;
            var wordCountAll = CountWords(wordsWithoutStopwords);
            wordCountSentences = 0;
            for (var i = 0; i < lengthOfDocuments; i++)
            {
                wordCountSentences++;
            }
            var IDFVals = new Dictionary<string, double>();
            wordCountSentencesLength = wordCountSentences;
            foreach (var uniqueWord in uniqueWordsSet)
            {
                var tempAdd = 0;
                foreach (var sentence in sentences)
                {
                    if (sentence.Contains(uniqueWord))
                    {
                        tempAdd += 1;
                    }
                }
                //IDFVals[uniqueWord] = Math.Log10(wordCountAll[uniqueWord] / tempAdd);
                // Check if tempAdd is zero to prevent division by zero
                double idfValue = tempAdd != 0 ? Math.Log10(wordCountAll[uniqueWord] / tempAdd) : 0.0;
                IDFVals[uniqueWord] = idfValue;
            }
            var IDFSentences = new Dictionary<string, double>();
            foreach (var sentence in sentences)
            {
                var sentenceSplitWords = sentence.Split(" ");
                var tempAdd = 0.0;
                var wordsNoStopWordsLength = Prettify(sentence).Count;
                foreach (var word in sentenceSplitWords)
                {
                    if (IDFVals.ContainsKey(word.ToLower()))
                    {
                        tempAdd += IDFVals[word.ToLower()];
                    }
                }
                IDFSentences[sentence] = tempAdd / wordsNoStopWordsLength;
            }
            return IDFSentences;
        }

        static List<string> TFIDF(string inputText)
        {
            var TFVals = TermFrequency(inputText);
            var IDFVals = InverseDocumentFrequency(inputText);
            var TFidfDict = new Dictionary<string, double>();
            foreach (var key in TFVals.Keys)
            {
                if (IDFVals.ContainsKey(key))
                {
                    TFidfDict[key] = TFVals[key] * IDFVals[key];
                }
            }
            var max = 0.0;
            var max2 = 0.0;
            var max3 = 0.0;
            var maxSentence = "";
            var max2Sent = "";
            var max3Sent = "";
            foreach (var key in TFidfDict.Keys)
            {
                if (TFidfDict[key] > max)
                {
                    max = TFidfDict[key];
                    maxSentence = key;
                }
                else if (TFidfDict[key] > max2 && TFidfDict[key] < max)
                {
                    max2 = TFidfDict[key];
                    max2Sent = key;
                }
                else if (TFidfDict[key] > max3 && TFidfDict[key] < max2 && TFidfDict[key] < max)
                {
                    max3 = TFidfDict[key];
                    max3Sent = key;
                }
            }
            return new List<string> { maxSentence, max2Sent, max3Sent };
        }
    }
}
