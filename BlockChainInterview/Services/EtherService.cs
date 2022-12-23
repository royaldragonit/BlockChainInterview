using BlockChainInterview.Models.EtherModels;
using BlockChainInterview.Utilities;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockChainInterview.Services
{
    public class EtherService : IEtherService
    {
        public EtherService()
        {

        }
        public GettBlockByNumberModel GetBlockByNumber(int number)
        {
            var hexNumber = $"0x{number:X}";
            var response = Api.GetEther<GettBlockByNumberModel>("eth_getBlockByNumber",hexNumber);
            return response;
        }
        public GettBlockTransactionCountByNumberModel GetBlockTransactionCountByNumber(string number)
        {
            var response = Api.GetEther<GettBlockTransactionCountByNumberModel>("eth_getBlockTransactionCountByNumber", number);
            return response;
        }
        public GetTransactionByBlockNumberAndIndex GetTransactionByBlockNumberAndIndex(string number,string index)
        {
            var response = Api.GetEther<GetTransactionByBlockNumberAndIndex>("eth_getTransactionByBlockNumberAndIndex", number,$"&index={index}");
            return response;
        }
    }
    public interface IEtherService
    {
        GettBlockByNumberModel GetBlockByNumber(int number);
        GettBlockTransactionCountByNumberModel GetBlockTransactionCountByNumber(string number);
        GetTransactionByBlockNumberAndIndex GetTransactionByBlockNumberAndIndex(string number, string index);
    }
}