using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, NAME, DETAIL
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
                    string sDetail = sInMessage;
                    aMessages.Add("Please input the student id of " + this.oOrder.Name+ " to know the" + sDetail);
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
