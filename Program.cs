using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BasketballData
{
    class Program
    {
        public class Node
        {
            public string last;
            public string first;
            public string age;
            public string min; // minutes played
            public string fieldGoal; // field goals
            public string fgAttempts; // field goals attempted

            public Node next;

            public Node(string tlast, string tfirst, string tage, string tmin, string tfieldGoal, string tfgAttempts)
            {
                last = tlast;
                first = tfirst;
                age = tage;
                min = tmin;
                fieldGoal = tfieldGoal;
                fgAttempts = tfgAttempts;
                next = null;
            }
        }

        public class LinkList
        {
            public Node head;
            public int count;
            public LinkList()
            {
                head = null;
            }

            public void addend(string tlast, string tfirst, string tage, string tmin, string tfieldGoal, string tfgAttempts)
            {
                Node temp = new Node(tlast, tfirst, tage, tmin, tfieldGoal, tfgAttempts);
                if (head == null)
                {
                    head = temp;
                    count++;
                }
                else
                {
                    Node cu = head;
                    while (cu.next != null)
                    {
                        cu = cu.next;
                    }
                    cu.next = temp;
                    count++;

                }
            }

            public void addbeg(string tlast, string tfirst, string tage, string tmin, string tfieldGoal, string tfgAttempts)
            {
                if (head == null)
                {
                    Node temp = new Node(tlast, tfirst, tage, tmin, tfieldGoal, tfgAttempts);
                    head = temp;
                    count++;
                }

                else
                {
                    Node temp = new Node(tlast, tfirst, tage, tmin, tfieldGoal, tfgAttempts);
                    temp.next = head;
                    head = temp;
                    count++;
                }
            }

            public void print(Node head)
            {
                if (head == null)
                {
                    return;
                }

                else
                {
                    Console.WriteLine(head.last + ", " + head.first + ", " + head.age + ", " + head.min + ", " + head.fieldGoal + ", " + head.fgAttempts);
                    print(head.next);
                }
            }

            public void readtxt(string file)
            {

                using (StreamReader sr = File.OpenText(file))
                {
                    string s = String.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        s = s.Replace(" ", String.Empty);
                        //delimit by comma
                        string[] tokens = s.Split(',');
                        //add to linked list
                        addend(tokens[0], tokens[1], tokens[2], tokens[3], tokens[4], tokens[5]);
                    }
                }
                print(head);

            }

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7P99TDD\MYMSSQLSERVER;Initial Catalog=Project1;Integrated Security=True;");
            SqlCommand cmd;
            public void sql()
            {
                //reads file and adds it to linked list

                con.Open();
                Node cu = head;
                while (cu.next != null)
                {
                    //Inserts values into SQL
                    cmd = new SqlCommand("insert into Basketball values('" + cu.last + "', '" + cu.first + "', '" + cu.age + "', '" + cu.min + "', '" + cu.fieldGoal + "', '" + cu.fgAttempts + "')", con);
                    cmd.ExecuteNonQuery();
                    cu = cu.next;
                }

                con.Close();
            }

        }


        static void Main(string[] args)
        {
            LinkList l = new LinkList();
            //calls class containing SQL command
            l.readtxt("inputdata.txt");
            l.sql();

            Console.ReadLine();
        }

    }
}
