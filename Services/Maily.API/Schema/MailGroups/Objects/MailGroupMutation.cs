﻿using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    public class MailGroupMutation
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailGroupMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Create and store an instance of <see cref="MailGroup"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of created <see cref="MailGroup"/>.</returns>
        public MailGroup CreateMailGroup(MailGroupCreateInput input)
        {
            var user = _tokenizer.GetUser();

            var mailGroup = new MailGroup()
            {
                Name = input.Name,
                UserId = user.Id
            };

            _context.Add(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }

        /// <summary>
        /// Update a stored instance of <see cref="MailGroup"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of updated <see cref="MailGroup"/>.</returns>
        public MailGroup UpdateMailGroup(MailGroupUpdateInput input)
        {
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == input.Id);

            if (mailGroup == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mailGroup.UserId != user.Id)
                return null;

            mailGroup.Name = input.Name;

            _context.Update(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }

        /// <summary>
        /// Delete a stored instance of <see cref="MailGroup"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of removed <see cref="MailGroup"/>.</returns>
        public MailGroup DeleteMailGroup(MailGroupDeleteInput input)
        {
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == input.Id);

            if (mailGroup == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mailGroup.UserId != user.Id)
                return null;

            _context.Remove(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }
    }
}
