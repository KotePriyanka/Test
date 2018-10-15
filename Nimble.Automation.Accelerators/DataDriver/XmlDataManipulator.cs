using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace  Nimble.Automation.Accelerators
{
    public class XmlDataManipulator
    {
        /// <summary>
        /// Returns a double dimentional array with element values of an XML file
        /// passed as String Parameter. The sample file assumed to be in the below
        /// format. <?xml version="1.0"?> <Class> <Student id="1">
        /// <firstname>Kiran</firstname> <lastname>Kumar</lastname>
        /// <salary>10000</salary> </Student> <Student id="2">
        /// <firstname>Ram</firstname> <lastname>Rahim</lastname>
        /// <salary>20000</salary> </Student> </Class>
        /// </summary>
        /// <returns> String[][] is a 2D array with tabular representation of data. </returns>
        public virtual string[][] getXMLDataIn2DArray(string filePath)
        {
            string[][] data = null;
            int rows = 0, cols = 0, p = 0, q = 0;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                doc.DocumentElement.Normalize();
                XmlNodeList allParentNodes = doc.DocumentElement.ChildNodes;
                XmlNodeList allChildNodes = allParentNodes.Item(1).ChildNodes; // item(0)
                // is
                // a
                // text
                // node.
                // and
                // item(1)
                // is
                // an
                // element
                // node
                // To find number of records which are strictly element nodes.
                for (int i = 0; i < allParentNodes.Count; i++)
                {
                    if (allParentNodes.Item(i).NodeType == XmlNodeType.Element)
                    {
                        rows++;
                    }
                }
                // To find number of columns which are strictly element nodes.
                for (int j = 0; j < allChildNodes.Count; j++)
                {
                    if (allChildNodes.Item(j).NodeType == XmlNodeType.Element)
                    {
                        cols++;
                    }
                }
                data = RectangularArrays.ReturnRectangularStringArray(rows, cols);
                for (int i = 0; i < allParentNodes.Count; i++)
                {
                    if (allParentNodes.Item(i).NodeType == XmlNodeType.Element)
                    {
                        // System.out.println(allNodes.item(i).getNodeName());
                        allChildNodes = allParentNodes.Item(i).ChildNodes;

                        for (int j = 0; j < allChildNodes.Count; j++)
                        {
                            if (allChildNodes.Item(j).NodeType == XmlNodeType.Element)
                            {
                                string item = allChildNodes.Item(j).InnerText;
                                //string item = allChildNodes.Item(j).ToString();
                                // System.out.println(item);
                                data[p][q] = item;
                                // System.out.println("\t"+allChildNodes.item(j).getTextContent());
                                q++;
                            }
                        }
                        p++;
                        q = 0;
                        // We use p,q instead of i,j to store skip the non-element
                        // node indexes in the array.
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return data;
            }
        }


        /// <summary>
        /// Creates an XML file with the Root element, and records name (parent node
        /// of all records) and the data from a 2D array.
        /// </summary>
        /// <param name="data">
        ///            : a 2D array with all tabular data to fill in text contents. </param>
        /// <param name="filePath">
        ///            : file name (with path) </param>
        /// <param name="rootEle">
        ///            : Root Element name. </param>
        /// <param name="recordNodeName">
        ///            : A record node name such as "Student" </param>
        /// <param name="columnNames">
        ///            : Elements inside recordNodeName such as "Name", "Roll Num",
        ///            etc.. </param>
        public virtual void createXML(string[][] data, string filePath, string rootEle, string recordNodeName, string[] columnNames)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement rootElement = xmlDoc.CreateElement(rootEle);
                xmlDoc.AppendChild(rootElement);
                for (int i = 0; i < data.Length; i++)
                {
                    XmlElement record = xmlDoc.CreateElement(recordNodeName);

                    rootElement.AppendChild(record); // Creates record node element
                    // such as "Student"
                    for (int j = 0; j < columnNames.Length; j++)
                    {

                        XmlElement ele = xmlDoc.CreateElement(columnNames[j]);
                        ele.AppendChild(xmlDoc.CreateTextNode(data[i][j]));
                        record.AppendChild(ele); // Creates Elements inside
                        // recordNodeName such as
                        // "Name", "Roll Num", etc..
                    }
                }
                xmlDoc.Save(filePath);
                //XslTransform transformer = new XslTransform();
                //System.IO.FileStream xsl = new System.IO.FileStream(xmlDoc.ToString(),System.IO.FileMode.Append);
                //System.IO.FileStream inStream = new System.IO.FileStream(xmlDoc.ToString(), System.IO.FileMode.Append);
                //transformer.Load(new XPathDocument(xsl), null, null);
                //XPathDocument doc = new XPathDocument(inStream);
                //System.IO.StreamWriter outStream = new System.IO.StreamWriter(filePath);
                //transformer.Transform(doc, null, outStream, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
    }
    internal static partial class RectangularArrays
    {
        internal static string[][] ReturnRectangularStringArray(int size1, int size2)
        {
            string[][] newArray;
            if (size1 > -1)
            {
                newArray = new string[size1][];
                if (size2 > -1)
                {
                    for (int array1 = 0; array1 < size1; array1++)
                    {
                        newArray[array1] = new string[size2];
                    }
                }
            }
            else
                newArray = null;

            return newArray;
        }
    }
}
