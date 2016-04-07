using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace RssReaderTestTaskByRelex
{
    public class Rss
    {
        public struct RssStruct
        {
            public string Url;
            public string Name;
            public int Id;
            public List<News> listRssNews;
        }

        public bool DownloadInternet = false;
        public List<RssStruct> ListRssChannel = new List<RssStruct>();
        
        private static string _stringConnection;
        
        public static void SetStringConnection(string strConn)
        {
            _stringConnection = strConn;
        }

        public bool CheckRssChannel()
        {
            if (ListRssChannel.Count == 0)
                return false;
            else
                return true;
        }

        // загрузка rss каналов в экземпляры класса из БД
        public void DownloadAllRssFromDataBase()
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT * FROM dbo.Rss_Channel";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    sqlConn.Open();

                    SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                    if (readerRssName.HasRows)
                    {
                        while (readerRssName.Read())
                        {
                            RssStruct tmpRss = new RssStruct();
                            tmpRss.Id = readerRssName.GetInt32(0);
                            tmpRss.Url = readerRssName.GetString(1);
                            tmpRss.Name = readerRssName.GetString(2);
                            tmpRss.listRssNews = new List<News>();
                            ListRssChannel.Add(tmpRss);
                        }
                    }
                    readerRssName.Close();
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // загрузка новостей в экземпляры класса из БД
        public void DownloadAllNewsFromDataBase()
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                try
                {
                    using (var sqlConn = new SqlConnection(_stringConnection))
                    {
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandText = "SELECT * FROM dbo.Rss_News WHERE RssId = @rssId ORDER BY PubDate DESC";
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.Connection = sqlConn;

                        SqlParameter sqlParam = new SqlParameter();
                        sqlParam.ParameterName = "@rssId";
                        sqlParam.Value = rss.Id;
                        sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                        sqlCmd.Parameters.Add(sqlParam);

                        sqlConn.Open();

                        SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                        if (readerRssName.HasRows)
                        {
                            while (readerRssName.Read())
                            {
                                News tmpNews = new News();
                                tmpNews.Id = readerRssName.GetInt32(0);
                                tmpNews.Title = readerRssName.GetString(2);
                                tmpNews.Discription = readerRssName.GetString(3);
                                tmpNews.Link = readerRssName.GetString(4);
                                tmpNews.PublicationDate = readerRssName.GetDateTime(5);

                                rss.listRssNews.Add(tmpNews);
                            }
                        }
                        readerRssName.Close();
                        sqlConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // название рассылки
        private string NameRss(string rssUrl)
        {
            System.Net.WebRequest myRequest = System.Net.WebRequest.Create(rssUrl);
            System.Net.WebResponse myResponse = myRequest.GetResponse();

            System.IO.Stream rssStream = myResponse.GetResponseStream();
            System.Xml.XmlDocument rssDoc = new System.Xml.XmlDocument();

            rssDoc.Load(rssStream);

            System.Xml.XmlNode rssDescription = rssDoc.SelectSingleNode("rss/channel/description");
            return rssDescription.InnerText;

        }

        // Сохранение новостей в список экземпляров класса News.
        private void ParseXml(RssStruct rss)
        {
            System.Net.WebRequest myRequest = System.Net.WebRequest.Create(rss.Url);
            System.Net.WebResponse myResponse = myRequest.GetResponse();

            System.IO.Stream rssStream = myResponse.GetResponseStream();
            System.Xml.XmlDocument rssDoc = new System.Xml.XmlDocument();

            rssDoc.Load(rssStream);

            System.Xml.XmlNode rssDescription = rssDoc.SelectSingleNode("rss/channel/description");
            rss.Name = rssDescription.InnerText;

            System.Xml.XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");

            for (int i = 0; i < rssItems.Count; i++)
            {
                News tmpNews = new News();
                System.Xml.XmlNode rssNode;

                rssNode = rssItems.Item(i).SelectSingleNode("title");
                if (rssNode != null)
                    tmpNews.Title = rssNode.InnerText;
                else
                    tmpNews.Title = "";

                rssNode = rssItems.Item(i).SelectSingleNode("description");
                if (rssNode != null)
                    tmpNews.Discription = rssNode.InnerText;
                else
                    tmpNews.Discription = "";

                rssNode = rssItems.Item(i).SelectSingleNode("link");
                if (rssNode != null)
                    tmpNews.Link = rssNode.InnerText;
                else
                    tmpNews.Link = "";

                rssNode = rssItems.Item(i).SelectSingleNode("pubDate");
                if (rssNode != null)
                    tmpNews.PublicationDate = DateTime.Parse(rssNode.InnerText);

                rss.listRssNews.Add(tmpNews);
            }
            rss.listRssNews.Sort((a, b) => b.PublicationDate.CompareTo(a.PublicationDate));
            this.DownloadInternet = true;
        }

        // загрузка новостей со всех рассылок (из интернета) в экземпляры класса
        public void DownloadAllNewsFromInternet()
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                try
                {
                    ParseXml(rss);
                    DownloadInternet = true;
                }
                catch (Exception ex)
                {
                    DownloadInternet = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //получение всей инофрмации по url ссылке во время добавления новой rss рассылки
        private RssStruct GetRssData(string urlAdress)
        {
            RssStruct rss = new RssStruct();
            rss.Url = urlAdress;
            rss.Name = NameRss(rss.Url);
            rss.listRssNews = new List<News>();
            try
            {
                ParseXml(rss);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DownloadInternet = false;
            }
            return rss;
        }


        // выгрузка в бд из экземпляра класса новых новостей
        public void LoadInDataBaseFromInternet()
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                try
                {
                    using (var sqlConn = new SqlConnection(_stringConnection))
                    {
                        bool found = false;

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.CommandText = "SELECT * FROM dbo.Rss_News WHERE RssId = @rssId ORDER BY PubDate DESC";
                        sqlCmd.Connection = sqlConn;

                        SqlParameter sqlParam = new SqlParameter();
                        sqlParam.ParameterName = "@rssId";
                        sqlParam.Value = rss.Id;
                        sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                        sqlCmd.Parameters.Add(sqlParam);

                        sqlConn.Open();

                        SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                        rssDataDataSetTableAdapters.Rss_NewsTableAdapter rssNews = new rssDataDataSetTableAdapters.Rss_NewsTableAdapter();

                        if (readerRssName.HasRows)
                            while (!found && readerRssName.Read())
                            {
                                foreach(News news in rss.listRssNews)
                                {
                                    if (news.Link != readerRssName.GetString(4) && news.PublicationDate > readerRssName.GetDateTime(5))
                                        rssNews.Insert(rss.Id, news.Title, news.Discription, news.Link, news.PublicationDate);
                                    else
                                        break;
                                }
                                found = true;
                            }
                        sqlConn.Close();
                        readerRssName.Close();

                        sqlConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // поиск в бд по url
        private bool FoundRss(string urlRss)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT RssAdress FROM dbo.Rss_Channel WHERE RssAdress = @urlRss";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@urlRss";
                    sqlParam.Value = urlRss;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerRssAdress = sqlCmd.ExecuteReader();

                    if (readerRssAdress.HasRows)
                        if (readerRssAdress.Read())
                            return true;
                    readerRssAdress.Close();
                    sqlConn.Close();
                    return false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        //получение id по rssName 
        private int GetRssIdByRssNameFromDataBase(string rssName)
        {
            int idResult = -1;
            bool found = false;
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT RssId FROM dbo.Rss_Channel WHERE RssName = @rssName";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@rssName";
                    sqlParam.Value = rssName;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                    if (readerRssName.HasRows)
                        if (!found && readerRssName.Read())
                        {
                            idResult = readerRssName.GetInt32(0);
                            found = true;
                        }
                    sqlConn.Close();
                    readerRssName.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return idResult;
        }

        // добавление названия рассылок в checkListBox
        public void LoadCheckList(CheckedListBox list)
        {
            //foreach (RssStruct rss in ListRssChannel)
            //    list.Items.Add(rss.Name);
            foreach (RssStruct rss in ListRssChannel)
            {
                if (list.FindStringExact(rss.Name) == -1)
                    list.Items.Add(rss.Name);
            }
        }

        //добавление новой рассылки в БД
        public void AddNewRssChannel(string urlAdress)
        {
           if (FoundRss(urlAdress))
                MessageBox.Show("Такая рассылка уже есть!");
           else
            {
                try
                {
                    rssDataDataSetTableAdapters.Rss_ChannelTableAdapter rssChannel = new rssDataDataSetTableAdapters.Rss_ChannelTableAdapter();
                    RssStruct tmpRss = GetRssData(urlAdress);
                    if (this.DownloadInternet)
                    {
                        rssChannel.Insert(tmpRss.Url, tmpRss.Name);
                        tmpRss.Id = GetRssIdByRssNameFromDataBase(tmpRss.Name);
                        rssDataDataSetTableAdapters.Rss_NewsTableAdapter rssNews = new rssDataDataSetTableAdapters.Rss_NewsTableAdapter();

                        foreach (News tmpNews in tmpRss.listRssNews)
                        {
                            rssNews.Insert(tmpRss.Id, tmpNews.Title, tmpNews.Discription, tmpNews.Link, tmpNews.PublicationDate);
                        }
                        ListRssChannel.Add(tmpRss);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить рассылку", "Внимание");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        
        //подписан ли юзер на эту новость
        private bool UserUseRss(int userId, int rssId)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT * FROM dbo.User_Rss_Channel WHERE UserId = @userId and RssId = @rssId";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@userId";
                    sqlParam.Value = userId;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@rssId";
                    sqlParam.Value = rssId;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerRssName = sqlCmd.ExecuteReader();
                    if (readerRssName.HasRows)
                        if (readerRssName.Read())
                            return true;
                    sqlConn.Close();
                    readerRssName.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        // id всех rss рассылок пользователя
        private bool GetRssIdToUserList(int rssId)
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                if (rss.Id == rssId)
                    return true;
            }
            return false;
        }

        //загрузка пользовательских расслок
        public void DownloadUserRssFromDataBase(int userId)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT * FROM dbo.Rss_Channel";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    sqlConn.Open();

                    SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                    if (readerRssName.HasRows)
                        while (readerRssName.Read())
                        {
                            if (!GetRssIdToUserList(readerRssName.GetInt32(0)) && UserUseRss(userId, readerRssName.GetInt32(0)))
                             {
                                RssStruct tmpRss = new RssStruct();
                                tmpRss.Id = readerRssName.GetInt32(0);
                                tmpRss.Url = readerRssName.GetString(1);
                                tmpRss.Name = readerRssName.GetString(2);
                                tmpRss.listRssNews = new List<News>();
                                ListRssChannel.Add(tmpRss);

                            }
                            
                        }
                    readerRssName.Close();
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 
        private bool GetNewsIdToUserList(int rssId, int NewsId)
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                if (rss.Id == rssId)
                    foreach (News tmpNews in rss.listRssNews)
                        if (tmpNews.Id == NewsId)
                            return true;
            }
            return false;
        }

        // загрузка пользовательских новостей из бд
        public void DownloadUserNewsFromDataBase()
        {
            foreach (RssStruct rss in ListRssChannel)
            {
                try
                {
                    using (var sqlConn = new SqlConnection(_stringConnection))
                    {
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandText = "SELECT * FROM dbo.Rss_News WHERE RssId = @rssId ORDER BY PubDate DESC";
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.Connection = sqlConn;

                        SqlParameter sqlParam = new SqlParameter();
                        sqlParam.ParameterName = "@rssId";
                        sqlParam.Value = rss.Id;
                        sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                        sqlCmd.Parameters.Add(sqlParam);

                        sqlConn.Open();

                        SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                        if (readerRssName.HasRows)
                            while (readerRssName.Read() && !GetNewsIdToUserList(rss.Id, readerRssName.GetInt32(0)))
                            {
                                    News tmpNews = new News();
                                    tmpNews.Id = readerRssName.GetInt32(0);
                                    tmpNews.Title = readerRssName.GetString(2);
                                    tmpNews.Discription = readerRssName.GetString(3);
                                    tmpNews.Link = readerRssName.GetString(4);
                                    tmpNews.PublicationDate = readerRssName.GetDateTime(5);
                                    rss.listRssNews.Add(tmpNews);                                
                            }
                        readerRssName.Close();
                        sqlConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // добавление не существующей ранее расслыки у пользователя в базу данныъ
        public void AddRssForUserInDataBase(int userId, string rssName)
        {
            rssDataDataSetTableAdapters.User_Rss_ChannelTableAdapter userRssChannel = new rssDataDataSetTableAdapters.User_Rss_ChannelTableAdapter();
            if (!UserUseRss(userId, GetRssIdByRssNameFromDataBase(rssName)))
                userRssChannel.Insert(userId, GetRssIdByRssNameFromDataBase(rssName));
        }

        // список рассылок пользователя для comboBox рассылок
        public List<string> AddChannelsToComboBox()
        {
            List<string> channelName = new List<string>();

            foreach (RssStruct channels in ListRssChannel)
            {
                if (channels.Name != null)
                   channelName.Add(channels.Name);
            }
            return channelName;
        }

        //при выборе rss возвращается список новостей, для comboBox новостей
        public List<string> SelectRss(int channelSelectedIndex)
        {
            List<string> titles = new List<string>();
            
            if (ListRssChannel[channelSelectedIndex].listRssNews[0] != null)
            {
                foreach (News userNews in ListRssChannel[channelSelectedIndex].listRssNews)
                {
                    if (!string.IsNullOrEmpty(userNews.Title))
                    {
                        titles.Add(userNews.Title);
                    }
                }
            }

            return titles;
        }

        // возвращает описание выбранной новости
        public string GetDescrition(int channelSelectedIndex, int titleSelectedIndex)
        {
            if (!string.IsNullOrEmpty(ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Discription))
            {
                return ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Discription;
            }
            else
                return "Нет описания данной новости";
        }

        // возвращает ссылку выбранной новости
        public string GetUrlLink(int channelSelectedIndex, int titleSelectedIndex)
        {
            if (!string.IsNullOrEmpty(ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Link))
            {
                return ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Title;
            }
            else
                return "Нет ссылки на новость";
        }

        // открытие новостной ссылки в браузере
        public void ClickLinkUrl(int channelSelectedIndex, int titleSelectedIndex)
        {
            if (ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Link != null)
                System.Diagnostics.Process.Start(ListRssChannel[channelSelectedIndex].listRssNews[titleSelectedIndex].Link);
        }

        // отмечаются те новости на которые подписан пользователь
        public void SelectUserRss(CheckedListBox rss)
        {
            int count = rss.Items.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (RssStruct channels in ListRssChannel)
                {
                    if (channels.Name == rss.Items[i].ToString())
                        rss.SetItemChecked(i, true);
                }
            }
        }

        // количество рассылок
        public int Count()
        {
            return ListRssChannel.Count();
        }

        // получение Id из таблицы User_Rss_Channel по userId и RssId
        private int GetIdUserRssChannel(int userId, int rssId)
        {
            int userRssChannelId = -1;
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT id FROM dbo.User_Rss_Channel WHERE UserId = @userId and RssId = @rssId";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@userId";
                    sqlParam.Value = userId;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@rssId";
                    sqlParam.Value = rssId;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerRssName = sqlCmd.ExecuteReader();

                    if (readerRssName.HasRows)
                        if (readerRssName.Read())
                            userRssChannelId = readerRssName.GetInt32(0);
                    readerRssName.Close();
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return userRssChannelId;
        }

        // отписка от тех рассылок (на которые подписан был ранее) которые не выбраны галочкой в checkedListBox
        // удаление из БД подписки и удаление из экземпляра класса
        public void DeleteRssForUserInDataBase(int userId, CheckedListBox rssCheckedListBox)
        {
            List<RssStruct> delChannels = new List<RssStruct>();
            foreach (RssStruct rss in ListRssChannel)
            {
                bool found = false;
                foreach (string channel in rssCheckedListBox.CheckedItems.OfType<string>())
                {
                    if (rss.Name == channel)
                        found = true;
                            
                }
                if (!found)
                {
                    rssDataDataSetTableAdapters.User_Rss_ChannelTableAdapter userRssChannel = new rssDataDataSetTableAdapters.User_Rss_ChannelTableAdapter();
                    if (UserUseRss(userId, rss.Id))
                    {
                        userRssChannel.Delete(GetIdUserRssChannel(userId, rss.Id), userId, rss.Id);
                        //listRssChannel.Remove(rss);
                        delChannels.Add(rss);
                    }
                }
            }

            foreach (RssStruct rss in delChannels)
            {
                ListRssChannel.Remove(rss);
            }
        }


        // Обновление новостей выбранного источника и добавление их в ComboBox и в список новостей.
        //public void UpgradeNews(ComboBox selectRss, ComboBox titleComboBox)
        public void UpgradeNews(int rssSelectedIndex)
        {
            if (ListRssChannel.Count != 0)
            {
                RssStruct tmpUpgradeNews = new RssStruct();
                tmpUpgradeNews.Url = ListRssChannel[rssSelectedIndex].Url;
                tmpUpgradeNews.listRssNews = new List<News>();
                ParseXml(tmpUpgradeNews);

                //bool found = false;
                int i = 0;


                foreach (News upgradeNews in tmpUpgradeNews.listRssNews)
                {
                    if (upgradeNews.PublicationDate > ListRssChannel[rssSelectedIndex].listRssNews[i].PublicationDate)
                    {
                        ListRssChannel[rssSelectedIndex].listRssNews.Insert(i, upgradeNews);
                        i++;
                    }
                    else
                        break;
                }
            }
        }
    }
}