using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using FluentAssertions;

namespace ConsoleApp1
{
    public class ReverseLinkedList
    {
        public static void Test()
        {
            new SinglyLinkedList(new[] { 1, 2, 3, 4, 5 }).Reverse().AsEnumerable().Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 }.Reverse(), options => options.WithStrictOrdering());
        }


        class SinglyLinkedListNode
        {
            public int data;
            public SinglyLinkedListNode next;

            public SinglyLinkedListNode(int nodeData)
            {
                this.data = nodeData;
                this.next = null;
            }
        }

        class SinglyLinkedList
        {
            public SinglyLinkedListNode head;
            public SinglyLinkedListNode tail;

            public SinglyLinkedList()
            {
                this.head = null;
                this.tail = null;
            }

            public SinglyLinkedList(IEnumerable<int> collections) : this()
            {
                foreach(int d in collections)
                {
                    InsertNode(d);
                }
            }            

            public void InsertNode(int nodeData)
            {
                SinglyLinkedListNode node = new SinglyLinkedListNode(nodeData);

                if (this.head == null)
                {
                    this.head = node;
                }
                else
                {
                    this.tail.next = node;
                }

                this.tail = node;
            }

            public SinglyLinkedList Reverse()
            {
                SinglyLinkedListNode prev = null;
                SinglyLinkedListNode current = head;
                SinglyLinkedListNode next = null;

                tail = head;
                while (current != null)
                {
                    next = current.next;
                    current.next = prev;
                    prev = current;
                    current = next;
                }       
                head = prev;


                return this;     
            }

            public IEnumerable<int> AsEnumerable()
            {
                var node = head;                
                while (node != null)
                {
                    yield return node.data;
                    node = node.next;
                }
            }            
        }        
    }
}