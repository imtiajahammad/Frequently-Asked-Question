using faqSystem02.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace faqSystem02.Controllers
{
    
    public class FaqModelController : Controller
    {

        

        // GET: FaqModel
        public ActionResult Index(int? page)
        {
            FaqModelContext faqModelContext = new FaqModelContext();
            List<FaqModel> faqModelList = faqModelContext.FaqModels.ToList();
            return View(faqModelList.ToPagedList(page?? 1,3));
        }


        public ActionResult CategoryButtonsAction(string categoryName, int? page)
        {
            if (categoryName == null)
            {
                categoryName = Session["CategoryQuery"].ToString();
            }
            else
            {
                Session["CategoryQuery"] = categoryName;
            }

            FaqModelContext faqModelContext = new FaqModelContext();
            List<FaqModel> thisList = faqModelContext.FaqModels.Where(emp => emp.category == categoryName).ToList();
            return View(thisList.ToPagedList(page ?? 1, 3));
        }


        public ActionResult SearchBy(string searchItem, int? page)
        {

            if(searchItem==null)
            {

                searchItem = Session["searchQuery"].ToString();
            }
            else
            {
                Session["searchQuery"]=searchItem;
            }

            

            /*
            
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
                    faqResult.id =(int) reader["id"];
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

            */
            FullTextQueryProcess obj = new FullTextQueryProcess();
            List<FaqModel> searchResultList = new List<FaqModel>();
            searchResultList= obj.queryProcessingMethod(searchItem);

            return View(searchResultList.ToPagedList(page ?? 1,3));
        }


        public ActionResult SingleQuestionView(int id)
        {
            FaqModelContext faqModelContext = new FaqModelContext();
            FaqModel faqSingleView = faqModelContext.FaqModels.Single(en => en.id == id);
            return View(faqSingleView);
            //List<FaqModel> thisList = faqModelContext.FaqModels.Where(emp => emp.category == categoryName).ToList();
        }
    }


}