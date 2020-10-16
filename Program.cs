using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballData
{
    class Program
    {
        class Node
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

        class LinkList
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
                    //already set to data and null
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
                        string[] tokens = s.Split(',');
                        addend(tokens[0], tokens[1], tokens[2], tokens[3], tokens[4], tokens[5]);
                    }
                }
                print(head);

            }

        }


        static void Main(string[] args)
        {
            //reading text file and adding to linked list;
            LinkList l = new LinkList();
            l.readtxt("inputdata.txt");



            Console.ReadLine();
        }

    }
}
