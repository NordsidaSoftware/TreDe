using System;
using System.Collections.Generic;

namespace TreDe
{

    // Dictionary containing the different bodyplans :
    // For now only one plan is available : the humanoid bodyplan
    // later more plans can be constructed, eventually also scripted via
    // a raw file
    public static class BodyPlans
    {
        public static Dictionary<int, BodyPlan> Plans = new Dictionary<int, BodyPlan>();
        public static void InitializeBodyPlans()
        {
            //    HUMANOID BODY PLAN 

            // Layer 0 :Setup the bodyplan
            BodyPlan Humanoid = new BodyPlan();

            // Layer 1 : setup a main bodypart
            BodyPart head = new BodyPart("head");
            // Layer 2 : setup a subpart
            BodyPart left_eye = new BodyPart("eye");
            head.subParts.Add(new Point3(-1, 1, 0), left_eye);

            BodyPart right_eye = new BodyPart("eye");
            head.subParts.Add(new Point3( 1, 1, 0), right_eye);

            Humanoid.BodyParts.Add(new Point3(0, 0, 0), head);


            BodyPart torso = new BodyPart("torso");
            Humanoid.BodyParts.Add(new Point3(0, 1, 0), torso);


            //     LOAD PLANS INTO STATIC DICTIONARY
            Plans.Add(0, Humanoid);       //  Humanoid = 0;
        }
    }

    public class BodyPlan
    {
        public Dictionary<Point3, BodyPart> BodyParts;

        public BodyPart this[Point3 p3] { get { return BodyParts[p3]; } }
        public BodyPlan()
        {
            BodyParts = new Dictionary<Point3, BodyPart>();
        }
    }
}