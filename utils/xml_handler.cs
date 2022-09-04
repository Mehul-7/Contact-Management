using System.Xml;

namespace Watchguard.Phonebook.XmlOperations{
    class XmlParse{

        private XmlElement? root{get; set;}
        public XmlParse(){
            XmlDocument doc = new XmlDocument();  
            doc.Load("config.xml"); 
            root = doc.DocumentElement;
        }
        
        internal string GetNodeValue(string tag){  
            XmlNodeList? elemList = root.GetElementsByTagName(tag);
            return elemList[0].InnerXml;
        }
    }
}