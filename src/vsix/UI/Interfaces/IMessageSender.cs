﻿namespace StartPagePlus.UI.Interfaces
{
    using System;
    using System.Threading.Tasks;

    //using GalaSoft.MvvmLight.Messaging;

    public interface IMessageSender
    {
        /// <summary>A method to safely run asynchronous code syncronously</summary>
        /// <remarks>Only use when the called code has an exception handler</remarks>
        /// <param name="asyncMethod">The asyncronous code to be run synchronously</param>
        /// <returns>A bool from the called code</returns>
        bool RunMethod(Func<Task<bool>> asyncMethod);

        /// <summary>A method to safely run asynchronous code syncronously</summary>
        /// <remarks>Only use when the called code has an exception handler</remarks>
        /// <param name="asyncMethod">The asyncronous code to be run synchronously</param>
        /// <returns>A bool? from the called code</returns>
        bool? RunMethod(Func<Task<bool?>> asyncMethod);

        //---

        /// <summary>A method to send a message</summary>
        /// <param name="message">The message to send</param>
        //void SendMessage(MessageBase message);
    }
}