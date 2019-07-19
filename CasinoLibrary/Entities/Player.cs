using System;

// Last modified 2019-04-16
namespace CasinoLibrary.Entities
{
    /// <summary>
    /// The Player class consists of player information used for BlackJack.
    /// </summary>
    [Serializable]
    public class Player
    {
        string userName, firstName, lastName, email, b64Pssword, b64Salt;
        int balance;
        static readonly int startingBalance = 10000; 

        /// <summary>
        /// Gets or sets a players username.
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (value != null || value != "")
                {
                    userName = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.UserName is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a players first name.
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != null || value != "")
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.FirstName is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a players last name.
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value != null || value != "")
                {
                    lastName = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.LastName is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a players email address.
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                if (value != null || value != "")
                {
                    email = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.Email is not valid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a players hashed password.
        /// </summary>
        public string B64Password
        {
            get { return b64Pssword; }
            set
            {
                if(value != null || value != "")
                {
                    b64Pssword = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.B64Password is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a hashed string which accompanies players password.
        /// </summary>
        public string B64Salt
        {
            get { return b64Salt; }
            set
            {
                if(value != null || value != "")
                {
                    b64Salt = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.B64Salt is invalid.");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the amount of money the player has.
        /// </summary>
        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (value >= 0)
                {
                    balance = value;
                }
                else
                {
                    throw new ArgumentException("Value for Player.Balance cannot be set below zero.");
                }
            }
        }

        /// <summary>
        /// Gets the amount of money the player starts with at registration.
        /// </summary>
        public static int STARTING_BALANCE
        {
            get { return startingBalance; }
        }

        /// <summary>
        /// The Constructor creates a new players with all field parameters.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="b64Password"></param>
        /// <param name="b64Salt"></param>
        /// <param name="balance"></param>
        public Player(string userName, string firstName, string lastName, string email, string b64Password, string b64Salt, int balance)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            B64Password = b64Password;
            B64Salt = b64Salt;
            Balance = balance;
        }

        /// <summary>
        /// This acts as a overloaded constructor that will be used for the Dealers inherited constructor.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="balance"></param>
        public Player(string userName, int balance)
        {
            UserName = userName;
            Balance = balance;
        }
    }
}