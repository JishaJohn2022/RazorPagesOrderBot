using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;
namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Welcome"));
        }
        [Fact]
        public void Testchildcare()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.ToLower().Contains("tiny tots"));
        }
        [Fact]
        public void TestName()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[1];
            Assert.True(sInput.ToLower().Contains("name"));
        }
        [Fact]
        public void TestDetail()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("john")[0];
            Assert.True(sInput.ToLower().Contains("detail"));
            //Assert.True(sInput.ToLower().Contains("large"));
        }
        [Fact]
        public void Teststudent()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("john");
            String sInput = oSession.OnMessage("fee")[0];
            Assert.True(sInput.ToLower().Contains("input"));
            Assert.True(sInput.ToLower().Contains("fee"));
            Assert.True(sInput.ToLower().Contains("student id"));


        }
    }
}
