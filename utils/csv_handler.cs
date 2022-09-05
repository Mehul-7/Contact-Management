using System;
using Watchguard.Phonebook.XmlOperations;

namespace Watchguard.Phonebook.CsvOperations{
    class CsvHandler{
        private string path=@"C:\WG Projects\C#\Contact Mgmt\contacts.csv";
        private XmlParse XmlObj=new XmlParse();
        internal void CsvWrite(List<string?> details, bool flag){
            if(!File.Exists(path)){
                File.WriteAllText(path, XmlObj.GetNodeValue("Header"));
            }
            using (StreamWriter writer = new StreamWriter(path, flag))
            {
                foreach (String? line in details)
                    writer.WriteLine(line);
            }
        }
        
        internal List<string?> CsvRead(int choice, string field, int flag){
            List<string?> lines=new();
            if(File.Exists(path)){
                using(StreamReader reader=new StreamReader(path)){
                    string? line;
                    while((line=reader.ReadLine())!=null){
                        if(line.Contains(",")){
                            String[] split=line.Split(',');
                            if(split[choice].Contains(field)){
                                line=String.Join(",",split);
                                if(flag==1)
                                    lines.Add(line);
                            }
                            else if(flag==0){
                                lines.Add(line);
                            }
                        }     
                    }
                }
            }
            return lines;
        }
    }
}