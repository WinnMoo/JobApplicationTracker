using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class ResetService
    {
        private ResetTokenRepository _resetTokenRepository;

        public ResetService(MongoClient db)
        {
            _resetTokenRepository = new ResetTokenRepository(db);
        }

        public bool InsertToken(PasswordResetToken token)
        {
            return _resetTokenRepository.InsertToken(token).Result;
        }

        public bool DeleteToken(string token)
        {
            return _resetTokenRepository.DeleteToken(token).Result;
        }

        public bool UpdateToken(PasswordResetToken token)
        {
            return _resetTokenRepository.UpdateToken(token).Result;
        }

        public PasswordResetToken GetToken(string token)
        {
            return _resetTokenRepository.GetToken(token).Result;
        }

        public ICollection<PasswordResetToken> GetTokensByUserId(ObjectId userId)
        {
            return _resetTokenRepository.GetTokens(userId);
        }
    }
}
