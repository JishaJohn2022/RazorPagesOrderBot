﻿using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, NAME, DETAIL, STUDENTID
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Tiny Tots Child Care!");
                     aMessages.Add("Please enter your child name, if the kid is a student here");
                    this.nCur = State.NAME;
                    break;
                case State.NAME:
                    this.oOrder.Name = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What detail would you like to know about   " + this.oOrder.Name );
                    this.nCur = State.DETAIL;
                    break;
                case State.DETAIL:
                    string sDetail = this.oOrder.Detail= sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Please input the student id of " + this.oOrder.Name+ " to know the " + sDetail);
                    this.nCur = State.STUDENTID;
                    break;
                case State.STUDENTID:
                    string sStudentid =this.oOrder.Studentid=sInMessage;
                    this.oOrder.Save();
                  aMessages.Add(" An email is send to the registered email address with " + this.oOrder.Detail+ " details of " + this.oOrder.Name);
                  aMessages.Add("Thank you for reaching out to Tiny Tots !");
                  this.nCur = State.WELCOMING;
                    break;

            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}