using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Web.Services;
using System.Xml;

namespace DistanceService
{
    /// <summary>
    ///     Summary description for DistanceWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DistanceWebService : WebService
    {
        private const string ConString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\Google Drive\\Visual Studio\\comp1690\\DistanceService\\distances.mdb";

        [WebMethod]
        public XmlDocument Distances()
        {
            // Base for XML
            var xmlDoc = new XmlDocument();
            XmlNode root = xmlDoc.CreateElement("distance");
            xmlDoc.AppendChild(root);

            try
            {
                // Get all distances
                var sql = SelectDistancesSql();
                var dset = DataSetFromSql(sql);

                XmlNode distanceNode = xmlDoc.CreateElement("distance");

                return DataSetToXmlDoc(dset, xmlDoc, root, true);
            }
            catch (Exception ex)
            {
                return ErrorMessageToXmlDoc(ex.Message, xmlDoc, root);
            }
        }

        [WebMethod]
        public XmlDocument Distance(string town1, string town2)
        {
            // Base for XML
            var xmlDoc = new XmlDocument();
            XmlNode root = xmlDoc.CreateElement("distance");
            xmlDoc.AppendChild(root);

            try
            {
                // Parameters validation
                if (String.IsNullOrEmpty(town1))
                {
                    return ErrorMessageToXmlDoc("Parameter town1 is required", xmlDoc, root);
                }
                if (String.IsNullOrEmpty(town2))
                {
                    return ErrorMessageToXmlDoc("Parameter town2 is required", xmlDoc, root);
                }

                // Search for specific instance between cities
                var sql = SelectDistancesSql() +
                          " WHERE (t1.Town = ? AND t2.Town = ?) " +
                          " OR (t1.Town = ? AND t2.Town = ?) ";
                var cmdParms = new OleDbParameter[4];
                cmdParms[0] = new OleDbParameter();
                cmdParms[0].Value = town1;
                cmdParms[1] = new OleDbParameter();
                cmdParms[1].Value = town2;
                cmdParms[2] = new OleDbParameter();
                cmdParms[2].Value = town2;
                cmdParms[3] = new OleDbParameter();
                cmdParms[3].Value = town1;
                var dset = DataSetFromSql(sql, cmdParms);
                DataSetToXmlDoc(dset, xmlDoc, root);

                return xmlDoc;
            }
            catch (Exception ex)
            {
                return ErrorMessageToXmlDoc(ex.Message, xmlDoc, root);
            }
        }

        private DataSet DataSetFromSql(string sql, params OleDbParameter[] cmdParms)
        {
            // Connection to DB
            var connect = new OleDbConnection(ConString);
            var command = new OleDbCommand(sql, connect);
            foreach (var parm in cmdParms)
            {
                command.Parameters.Add(parm);
            }

            try
            {
                connect.Open();

                // Get data from DB
                var da = new OleDbDataAdapter(command);
                var dset = new DataSet();
                da.Fill(dset);

                return dset;
            }
            finally
            {
                connect.Close();
            }
        }

        private XmlDocument DataSetToXmlDoc(DataSet dset, XmlDocument xmlDoc, XmlNode root, bool wrap = false)
        {
            foreach (DataTable table in dset.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    // Town 1
                    XmlNode town1Node = xmlDoc.CreateElement("town_1");
                    var idAttr1 = xmlDoc.CreateAttribute("id");
                    idAttr1.Value = dr["TownId1"].ToString();
                    town1Node.Attributes.Append(idAttr1);
                    XmlNode townName1 = xmlDoc.CreateElement("name");
                    townName1.InnerText = dr["TownName1"].ToString();
                    town1Node.AppendChild(townName1);
                    // Town 2
                    XmlNode town2Node = xmlDoc.CreateElement("town_2");
                    var idAttr2 = xmlDoc.CreateAttribute("id");
                    idAttr2.Value = dr["TownId2"].ToString();
                    town2Node.Attributes.Append(idAttr2);
                    XmlNode townName2 = xmlDoc.CreateElement("name");
                    townName2.InnerText = dr["TownName2"].ToString();
                    town2Node.AppendChild(townName2);
                    // Distance (between 2 town in miles)
                    XmlNode distanceMiles = xmlDoc.CreateElement("distance_miles");
                    distanceMiles.InnerText = dr["DistanceInMiles"].ToString();

                    // Attach elements to parent DOM
                    XmlNode parentNode;
                    if (wrap)
                    {
                        XmlNode distanceNode = xmlDoc.CreateElement("distance");
                        root.AppendChild(distanceNode);
                        parentNode = distanceNode;
                    }
                    else
                    {
                        parentNode = root;
                    }
                    parentNode.AppendChild(town1Node);
                    parentNode.AppendChild(town2Node);
                    parentNode.AppendChild(distanceMiles);
                }
            }

            return xmlDoc;
        }

        private XmlDocument ErrorMessageToXmlDoc(string message, XmlDocument xmlDoc, XmlNode root)
        {
            XmlNode rootNode = xmlDoc.CreateElement("message");
            rootNode.InnerText = message;
            root.AppendChild(rootNode);
            return xmlDoc;
        }

        private string SelectDistancesSql()
        {
            return "SELECT d.DistanceInMiles," +
                   "t1.Town AS TownName1," +
                   "t1.TownId AS TownId1," +
                   "t2.Town AS TownName2," +
                   "t2.TownId AS TownId2 " +
                   " FROM (Distance AS d INNER JOIN Town AS t1 ON d.Town1 = t1.TownId) " +
                   " INNER JOIN Town AS t2 ON d.Town2 = t2.TownId ";
        }
    }
}
