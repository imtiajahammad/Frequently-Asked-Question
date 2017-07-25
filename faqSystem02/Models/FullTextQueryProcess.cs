using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace faqSystem02.Models
{
    public class FullTextQueryProcess
    {

        public List<FaqModel> queryProcessingMethod(string query)
        {

            string processingComma = query.Replace(",", "");
            processingComma = processingComma.Replace(".", "");
            processingComma = processingComma.Replace("?", "");

            string cleanedString = System.Text.RegularExpressions.Regex.Replace(processingComma, @"\s+", " "); //to reduce multiple space



            

            string processingString = cleanedString.Replace(" ", ",");
            //System.out.println("last char = " + str.charAt(str.length() - 1));
            
            while (!Char.IsLetterOrDigit( processingString[processingString.Length-1]) )
            {
                processingString = processingString.Remove(processingString.Length - 1);
            }


            SqlConnection con = new SqlConnection(@"Server=IMTIAJ-PC;Database=faq01;Integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand("select * from table_info2 where CONTAINS(question,'formsof(INFLECTIONAL," + processingString + ")')", con);
            //SqlCommand cmdp = new SqlCommand("select * from table_info2 where question LIKE '%how%' or question LIKE '%to%' or question LIKE '%resolve%' or question LIKE '%result%'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<FaqModel> searchResultList = new List<FaqModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FaqModel faqResult = new FaqModel();
                    faqResult.id = (int)reader["id"];
                    faqResult.question = reader["question"].ToString();
                    faqResult.answer = reader["answer"].ToString(); ;
                    faqResult.category = reader["category"].ToString();
                    try
                    {
                        faqResult.askedAmount = (int?)reader["askedAmount"];
                    }
                    catch (Exception e)
                    { }

                    searchResultList.Add(faqResult);
                }
                reader.Close();
            }
            con.Close();

            return searchResultList;
        }
    }


}