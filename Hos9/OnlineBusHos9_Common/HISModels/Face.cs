using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OnlineBusHos9_Common.HISModels
{
    internal class rlsb11
    {

        public class result11
        {
            public string msg { get; set; }
            public int code { get; set; }
            public Data data { get; set; }
            public string reqno { get; set; }
        }

        public class Data
        {
            public string result { get; set; }
        }
    }

    internal class rlsb81
    {
        public class result81
        {
            public string msg { get; set; }
            public int code { get; set; }
            public Data data { get; set; }
            public string reqno { get; set; }
        }

        public class Data
        {
            public Result[] result { get; set; }
        }

        public class Result
        {
            public Bounding_Box bounding_box { get; set; }
            public float similarity { get; set; }
            public string external_image_id { get; set; }
            public string face_id { get; set; }
        }

        public class Bounding_Box
        {
            public int width { get; set; }
            public int top_left_y { get; set; }
            public int top_left_x { get; set; }
            public int height { get; set; }
        }

    }
    internal class rlsb45
    {
        public class rusult45
        {
            public int code { get; set; }
            public Data data { get; set; }
            public string msg { get; set; }
            public string reqno { get; set; }
        }

        public class Data
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public bool alive { get; set; }
            public float confidence { get; set; }
            [JsonIgnore]
            public string picture { get; set; }
        }
    }
    internal class rlsb83
    {
        public class result83
        {
            public string msg { get; set; }
            public int code { get; set; }
            public Data data { get; set; }
            public string reqno { get; set; }
        }

        public class Data
        {
            public Result[] result { get; set; }
        }

        public class Result
        {
            public Bounding_Box bounding_box { get; set; }
            public string external_image_id { get; set; }
            public string face_id { get; set; }
        }

        public class Bounding_Box
        {
            public int width { get; set; }
            public int top_left_y { get; set; }
            public int top_left_x { get; set; }
            public int height { get; set; }
        }

    }
    internal class rlsb82
    {

        public class result82
        {
            public string msg { get; set; }
            public int code { get; set; }
            public Data data { get; set; }
            public string reqno { get; set; }
        }

        public class Data
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public int face_number { get; set; }
            public string face_set_id { get; set; }
            public string face_set_name { get; set; }
            public string create_date { get; set; }
            public int face_set_capacity { get; set; }
        }

    }


}
