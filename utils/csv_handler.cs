using System;

namespace Watchguard.Phonebook.CsvOperations{
    class CsvHandler{
        private string path=@"C:\WG Projects\C#\Contact Mgmt\contacts.csv";

        internal void CsvWrite(List<string?> details){
            string delimiter=", ";
            if(!File.Exists(path)){
                string header= "Name" + delimiter + "Mobile No." + delimiter + "Email" + delimiter + Environment.NewLine;
                File.WriteAllText(path, header);
            }
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (String? line in details)
                    writer.WriteLine(line);
            }
        }
        internal void CsvRead(int choice, string field, int flag){
            if(File.Exists(path)){
                List<string?> lines=new();
                using(StreamReader reader=new StreamReader(path)){
                    string? line;
                    while((line=reader.ReadLine())!=null){
                        if(line.Contains(",")){
                            String[] split=line.Split(',');
                            if(split[choice-1].Contains(field)){
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
                if(flag==0){
                    CsvWrite(lines);
                }
                else{
                    foreach (var line in lines){
                        Console.WriteLine(line);
                    }
                }
            }
        }
    }
}