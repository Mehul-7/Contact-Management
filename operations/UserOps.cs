using System;
using Figgle;
using System.Xml;
using Watchguard.Phonebook.CsvOperations;
using Watchguard.Phonebook.XmlOperations;

namespace Watchguard.Phonebook.actions{
    class AddUser{
        private XmlParse XmlObj=new XmlParse();
        internal void addUser(){ 
            List<string> NodeTags=new List<string>(){
                "Name_Prompt",
                "Mobile_No",
                "Email_Prompt"
            }; 
            (string? UserName, string? MobileNo, string? EmailId)=GetUserDetails(NodeTags);
            List<string?> details=new List<string?>
            {
                $"{UserName}, {MobileNo}, {EmailId}, "
            };
            CsvHandler obj=new();
            obj.CsvWrite(details, true);
        }

        internal void editContact(){
            string path=@"C:\WG Projects\C#\Contact Mgmt\resources\contacts.csv";
            List<string?> lines=new List<string?>();
            List<string> NodeTags=new List<string>(){
                "Get_Name",
                "Edit_Field",
                "Get_Value"
            }; 
            Console.WriteLine(FiggleFonts.Standard.Render("EDIT MENU"));
            (string? UserName, string? ChoiceStr, string? NewValue)=GetUserDetails(NodeTags);
            int Choice=Convert.ToInt32(ChoiceStr);
            if(!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(NewValue)){
                if(File.Exists(path)){
                    using(StreamReader reader = new(path)){
                        String? line;
                        while((line=reader.ReadLine()) != null){
                            if(line.Contains(",")){
                                String[] split=line.Split(',');
                                if(split[0].Contains(UserName)){
                                    split[Choice]=NewValue;
                                    line=String.Join(",",split);
                                }
                            }
                            lines.Add(line);
                        }
                    }
                    CsvHandler obj=new();
                    obj.CsvWrite(lines, false);
                }
            }
        }

        internal void searchContact(){
            Console.WriteLine(FiggleFonts.Standard.Render("SEARCH OPTIONS"));
            (int choice, string? field)=GetSearchField();
            Console.WriteLine();
            if(!string.IsNullOrEmpty(field)){
                CsvHandler obj=new();
                List<string?> lines=obj.CsvRead(choice, field, 1);
                foreach (var line in lines){
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
        }

        internal void deleteContact(){
            Console.WriteLine(FiggleFonts.Standard.Render("DELETE OPTIONS"));
            (int choice, string? field)=GetSearchField();
            if(!string.IsNullOrEmpty(field)){
                CsvHandler obj=new();
                List<string?> lines=obj.CsvRead(choice, field, 0);
                obj.CsvWrite(lines, false);
            }
        }

        internal (int, string?) GetSearchField(){
            Console.WriteLine(XmlObj.GetNodeValue("Get_Choice"));
            int choice=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(XmlObj.GetNodeValue("Get_Value"));
            string? field=Console.ReadLine();
            return (choice, field);
        }

        internal Tuple<string?, string?, string?> GetUserDetails(List<string> NodeTag){
            Console.WriteLine(XmlObj.GetNodeValue(NodeTag[0]));
            string? value1=Console.ReadLine();
            Console.WriteLine(XmlObj.GetNodeValue(NodeTag[1]));
            string? value2=Console.ReadLine();
            Console.WriteLine(XmlObj.GetNodeValue(NodeTag[2]));
            string? value3=Console.ReadLine();
            var Value=Tuple.Create(value1, value2, value3);
            return Value;
        }
    }
}