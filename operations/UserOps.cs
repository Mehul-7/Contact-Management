using System;
using System.Xml;
using Watchguard.Phonebook.CsvOperations;
using Watchguard.Phonebook.XmlOperations;

namespace Watchguard.Phonebook.actions{
    class AddUser{
        private XmlParse XmlObj=new XmlParse();
        internal void addUser(){  
            Console.WriteLine(XmlObj.GetNodeValue("Name_Prompt"));
            string? user_name=Console.ReadLine();
            Console.WriteLine(XmlObj.GetNodeValue("Mobile_No"));
            string? mobile_no=Console.ReadLine();
            Console.WriteLine(XmlObj.GetNodeValue("Email_Prompt"));
            string? email_id=Console.ReadLine();
            List<string?> details=new List<string?>
            {
                $"{user_name}, {mobile_no}, {email_id}, "
            };
            CsvHandler obj=new();
            obj.CsvWrite(details);
        }

        internal void editContact(){
            string path=@"C:\WG Projects\C#\Contact Mgmt\contacts.csv";
            List<string?> lines=new List<string?>();
            Console.WriteLine(XmlObj.GetNodeValue("Get_Name"));
            var userName=Console.ReadLine();
            Console.WriteLine(XmlObj.GetNodeValue("Get_Choice"));
            int choice=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(XmlObj.GetNodeValue("Get_Value"));
            var newVal=Console.ReadLine();
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(newVal)){
                if(File.Exists(path)){
                    using(StreamReader reader = new(path)){
                        String? line;
                        while((line=reader.ReadLine()) != null){
                            if(line.Contains(",")){
                                String[] split=line.Split(',');
                                if(split[0].Contains(userName)){
                                    split[choice]=newVal;
                                    line=String.Join(",",split);
                                }
                            }
                            lines.Add(line);
                        }
                    }
                    CsvHandler obj=new();
                    obj.CsvWrite(lines);
                }
            }
        }

        internal void searchContact(){
            (int choice, string? field)=GetSearchField();
            if(!string.IsNullOrEmpty(field)){
                CsvHandler obj=new();
                obj.CsvRead(choice, field, 1);
            }
        }

        internal void deleteContact(){
            (int choice, string? field)=GetSearchField();
            if(!string.IsNullOrEmpty(field)){
                CsvHandler obj=new();
                obj.CsvRead(choice, field, 0);
            }
        }

        internal (int, string?) GetSearchField(){
            Console.WriteLine(XmlObj.GetNodeValue("Get_Choice"));
            int choice=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(XmlObj.GetNodeValue("Get_Value"));
            string? field=Console.ReadLine();
            return (choice, field);
        }
    }
}