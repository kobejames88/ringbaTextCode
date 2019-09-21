using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Net;
namespace myApp
{
class Program
{
    static void Main(string[] args)
    {
    	 UpperLetter();//Calling method

    }
   public static void UpperLetter(){
     	int capital = 0;
        int lowercase = 0;
        WebClient client = new WebClient();
        //byte[] buffer = client.DownloadData("https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt");
        //string str = Encoding.GetEncoding("GB2312").GetString(buffer);
        string str = Encoding.ASCII.GetString(client.DownloadData(@"https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt"));
        //string str=Get("https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt");
        //string str = File.ReadAllText(@"d:\output.txt"); //Local path
        int en = Regex .Matches ( str , "[A-Za-z]" ) .Count;//Number of letters per statistic file
        MatchCollection mcreword;//Word Matching Set
        MatchCollection mcreprx;//Word prefix matching set
        //String wordTop=Regex.Matches(str,"[A-Za-z]");
        Regex reword = new Regex("[A-Z]+[a-z]+");//Regular expressions match words
        mcreword=reword.Matches(str);
        string[] wordArray = new string[mcreword.Count];//Matching set to string format
        for (int i = 0; i < mcreword.Count;i++ )
         {
            wordArray[i] = mcreword[i].Groups[0].Value;
          
         }
        Regex reprx = new Regex("[A-Z]+[a-z]");//Regular expressions match word prefixes
        mcreprx=reprx.Matches(str);
        string[] preArray = new string[mcreprx.Count];//Matching set to string format
         for (int i = 0; i < mcreprx.Count;i++ )
         {
            preArray[i] = mcreprx[i].Groups[0].Value;
          
         }
        char[] chars = str.ToCharArray();
        foreach(char num in chars){//Match the number of uppercase letters
            if(num>='A' && num<='Z'){
                   capital++;
            }else if(num>='a' && num<='z'){
                   lowercase++;
            }
        }
        Dictionary<String, int> wordTop = new Dictionary<String, int>();//Value of Dictionary Statistical Words
        
             foreach(String word in wordArray){
             if(wordTop.Keys.Contains(word)){
             	    wordTop[word]++; //Add 1 to the words that appear
                 }else{
                     wordTop.Add(word,1); 
                 }
             	
             }
             getWordAsc(wordTop);//Call sort method

          Dictionary<String, int> prefixTop = new Dictionary<String, int>();
             foreach(String prefix in preArray){

               if(prefixTop.Keys.Contains(prefix)){
               	       prefixTop[prefix]++;
                 }else{		
                 	 prefixTop.Add(prefix,1);   
                 }
             	
             }

              getPrefixAsc(prefixTop);

      
    
           // foreach (KeyValuePair<String, int> entry in wordTop)
           //  {
           //     String word = entry.Key;
           //     int frequency = entry.Value;
           //     Console.WriteLine("{0}: {1}", word, frequency);
           //  }
         
       

          // foreach (KeyValuePair<String, int> entry in prefixTop)
          // {
          //   String word = entry.Key;
          //   int frequency = entry.Value;
          //   Console.WriteLine("{0}: {1}", word, frequency);
          //  }


        Console.WriteLine("Letters："+en);
        Console.WriteLine("Capitalized letters："+capital);
        //Console.WriteLine("Lowercase letters：" + count2);
        Console.ReadKey();
   }
   
  public static void getPrefixAsc(Dictionary<String,int>getdictionary) {//Word prefix Sorting Method

  List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(getdictionary);

        myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) {

                return s2.Value.CompareTo(s1.Value);

            });

        getdictionary.Clear();

        foreach (KeyValuePair<string, int> pair in myList){

            getdictionary.Add(pair.Key, pair.Value);

        }

        // foreach (KeyValuePair<string, int> pair in myList) {

        //     //Console.WriteLine(prefixTop[key]);
        //     Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
        // }

        KeyValuePair<string, int> kvp=myList.First();
        //var firstString=myList.First();
        //Console.WriteLine(""+firstString);
        Console.WriteLine("The most common 2 character prefix：{0} The number of occurrences：{1}",kvp.Key,kvp.Value);


        }

       public static void getWordAsc(Dictionary<String,int>getdictionary) {//Word Sorting Method

       List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(getdictionary);

        myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) {

                return s2.Value.CompareTo(s1.Value);

            });

        getdictionary.Clear();

        foreach (KeyValuePair<string, int> pair in myList){

            getdictionary.Add(pair.Key, pair.Value);

        }

        KeyValuePair<string, int> kvp=myList.First();
       
        Console.WriteLine("The most common word：{0} The number of times：{1}",kvp.Key,kvp.Value);


        }

       public static string Get(string strURL) //Connecting the Amazon Cloud Method
        {
            HttpWebRequest request;
            // Create an HTTP request
            request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }


     }
  
      

  }

