using System;
using Ostis.Sctp;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;
using Ostis.Sctp.Arguments;
using System.Diagnostics;
using System.Collections.Generic;


namespace Ostis.SctpDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
		
//			List<ScAddress> adresses = new List<ScAddress> ();
//			Console.WriteLine ("Start to create 10000 nodes");
//			CommandPool pool = new CommandPool ("127.0.0.1", 55770, ClientType.SyncClient);
//			Stopwatch watch = new  Stopwatch ();
//			watch.Start ();
//
//			for (int i=0; i<10000; i++) {
//				ACommand cmd_create_node = Command.CreateNode (ElementType.sc_type_node_const);
//				pool.Send (cmd_create_node);
//				adresses.Add ((cmd_create_node.Response as RspCreateNode).CreatedNodeAddress);
//			}
//
//			foreach (ScAddress adr in adresses ){
//				ACommand cmd_delnode = Command.DeleteElement (adr);
//				pool.Send (cmd_delnode);
//			}
//
//
//			watch.Stop ();
//			Console.WriteLine ("Times elapsed: {0}", watch.Elapsed.ToString());
			Demo demo_test = new Demo ();
			demo_test.demoCreateNode ();

		}
	}
}
