using System;
using Figgle;
using Watchguard.Phonebook.actions;
using Watchguard.Phonebook.XmlOperations;

class Entry{
    
    static void Main(){
        XmlParse XmlObj=new XmlParse();
        int choice=0;
        while(choice!=5){
            Console.WriteLine(FiggleFonts.Standard.Render("WELCOME USER"));
            Console.WriteLine(XmlObj.GetNodeValue("user_menu"));
            choice=Convert.ToInt32(Console.ReadLine());
            AddUser obj=new();
            switch (choice)
            {
                case 1:
                    obj.addUser();
                    break;
                case 2:
                    obj.editContact();
                    break;
                case 3:
                    obj.deleteContact();
                    break;
                case 4:
                    obj.searchContact();
                    break;
                case 5: 
                    Console.WriteLine("Program Terminated");
                    break;
                default:
                    Console.WriteLine("WRONG INPUT");
                    break;
            }
        }
    }
}
