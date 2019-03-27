using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G10_ProjectDotNet.Models.Domain
{
    // Bevat de definition van Session en alle sessionstates
    public class Session
    {
        public int SessionId { get; set; }
        public Weekday Day { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public string StateSerialized { get; set; }
        public SessionState State { get; set; }

        public Session()
        {

        }

        public void AddAttendance(Attendance attendance)
        {
            //State.RegisterAttendance(attendance);
            if (AlreadyRegistered(attendance.MemberId))
            {
                throw new InvalidOperationException();
            }
            Attendances.Add(attendance);
        }

        public void EndRegistration()
        {
            ChangeState(new RegistrationEndedState(this));
        }

        public void EndSession()
        {
            ChangeState(new SessionEndedState(this));
        }

        public bool AlreadyRegistered(int memberId)
        {
            return !(Attendances.Where(b => b.MemberId == memberId).SingleOrDefault() == null);
        }

        protected void ChangeState(SessionState state)
        {
            State = state;
            StateSerialized = JsonConvert.SerializeObject(State.GetType());
        }
    }

    public abstract class SessionState
    {
        protected Session Session { get; set; }

        public SessionState(Session session)
        {
            Session = session;
        }

        public abstract void RegisterAttendance(Attendance attendance);
    }

    public class RegistrationState : SessionState
    {
        public RegistrationState(Session session): base(session)
        {

        }
        public override void RegisterAttendance(Attendance attendance)
        {
            if (Session.AlreadyRegistered(attendance.MemberId))
            {
                throw new InvalidOperationException();
            }
            Session.AddAttendance(attendance);
        }
    }

    public class RegistrationEndedState : SessionState
    {
        public RegistrationEndedState(Session session): base(session)
        {
        }

        public override void RegisterAttendance(Attendance attendance)
        {
            throw new Exception("Registration has ended");
        }
    }

    public class SessionEndedState : SessionState
    {
        public SessionEndedState(Session session): base(session)
        {

        }

        public override void RegisterAttendance(Attendance attendance)
        {
            throw new Exception("Session has ended");
        }
    }

}
