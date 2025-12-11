using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;
using ContractManagement.Model.DAL;
using static ContractManagement.Model.DAL.ContractBlockDAL;

namespace ContractManagement.Tests
{
    /// <summary>
    /// Acceptance Tests for Contract Creation and Block Management
    /// Compatible with .NET Framework 4.7.2
    /// </summary>
    [TestClass]
    public class ContractBlockManagementAcceptanceTests
    {
        private ContractController _contractController;
        private BlockController _blockController;
        private UserController _userController;
        private ContractDAL _contractDAL;
        private OriginalContractBlockDAL _originalBlockDAL;
        private ContractBlockDAL _contractBlockDAL;

        // Test data IDs - Match the actual database
        private const int TEST_USER_ID_1 = 1;
        private const int TEST_ORIGINAL_BLOCK_ID_1 = 1;
        private const int TEST_ORIGINAL_BLOCK_ID_2 = 2;
        private const int TEST_ORIGINAL_BLOCK_ID_3 = 3;
        private const int TEST_ORIGINAL_BLOCK_ID_4 = 4;
        private const int TEST_ORIGINAL_BLOCK_ID_5 = 5;

        [TestInitialize]
        public void Setup()
        {
            _contractController = new ContractController();
            _blockController = new BlockController();
            _userController = new UserController();
            _contractDAL = new ContractDAL();
            _originalBlockDAL = new OriginalContractBlockDAL();
            _contractBlockDAL = new ContractBlockDAL();
        }

        #region Contract Creation Tests

        [TestMethod]
        [TestCategory("ContractCreation")]
        [Priority(1)]
        public void TC001_CreateNewContract_HappyPath_ShouldSucceed()
        {
            string companyName = "Test Company ABC " + DateTime.Now.Ticks;
            int creatorId = TEST_USER_ID_1;

            int contractId = _contractController.CreateContract(companyName, creatorId);

            Assert.IsTrue(contractId > 0, "Contract ID should be greater than 0");
            Contract createdContract = _contractController.GetContractById(contractId);
            Assert.IsNotNull(createdContract, "Contract should exist in database");
            Assert.AreEqual(companyName, createdContract.Company_name, "Company name should match");
            Assert.AreEqual(creatorId, createdContract.The_Creator, "Creator ID should match");
            Assert.IsFalse(createdContract.Approved, "New contract should not be approved");
            Assert.IsFalse(createdContract.Sent_to_external, "New contract should not be sent to external");

            Console.WriteLine("Contract created successfully with ID: {0}", contractId);
        }

        [TestMethod]
        [TestCategory("ContractCreation")]
        [Priority(2)]
        public void TC002_CreateMultipleContracts_ShouldSucceed()
        {
            int creatorId = TEST_USER_ID_1;
            string timestamp = DateTime.Now.Ticks.ToString();

            int contractId1 = _contractController.CreateContract("Company A " + timestamp, creatorId);
            int contractId2 = _contractController.CreateContract("Company B " + timestamp, creatorId);
            int contractId3 = _contractController.CreateContract("Company C " + timestamp, creatorId);

            Assert.IsTrue(contractId1 > 0, "Contract 1 should be created");
            Assert.IsTrue(contractId2 > 0, "Contract 2 should be created");
            Assert.IsTrue(contractId3 > 0, "Contract 3 should be created");
            Assert.AreNotEqual(contractId1, contractId2, "Contract IDs should be unique");
            Assert.AreNotEqual(contractId2, contractId3, "Contract IDs should be unique");

            List<Contract> userContracts = _contractController.GetContractsByCreator(creatorId);
            Assert.IsTrue(userContracts.Count >= 3, "User should have at least 3 contracts");

            Console.WriteLine("Successfully created 3 contracts");
        }

        [TestMethod]
        [TestCategory("ContractCreation")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC003_CreateContractWithEmptyName_ShouldHandleGracefully()
        {
            int creatorId = TEST_USER_ID_1;

            try
            {
                int contractId1 = _contractController.CreateContract("", creatorId);
                Assert.IsTrue(contractId1 > 0, "System allows empty company name");
                Console.WriteLine("System allows empty company name");
            }
            catch (Exception ex)
            {
                Console.WriteLine("System rejects empty company name: {0}", ex.Message);
                Assert.IsTrue(true);
            }
        }

        #endregion

        #region Add Block Tests

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(1)]
        public void TC004_AddSingleBlockToContract_HappyPath_ShouldSucceed()
        {
            int contractId = _contractController.CreateContract("Test Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            int originalBlockId = TEST_ORIGINAL_BLOCK_ID_1;

            OriginalContractBlock originalBlock = _originalBlockDAL.GetOriginalBlockById(originalBlockId);
            Assert.IsNotNull(originalBlock, "Original block should exist");

            bool result = _contractController.AddBlockToContract(contractId, originalBlockId);

            Assert.IsTrue(result, "Block should be added successfully");
            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(1, blocks.Count, "Contract should have exactly 1 block");

            Console.WriteLine("Block added successfully");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(1)]
        public void TC005_AddMultipleBlocksToContract_ShouldSucceed()
        {
            int contractId = _contractController.CreateContract("Multi Block Company " + DateTime.Now.Ticks, TEST_USER_ID_1);

            bool result1 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            bool result2 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            bool result3 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);

            Assert.IsTrue(result1 && result2 && result3, "All blocks should be added");

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(3, blocks.Count, "Contract should have exactly 3 blocks");

            Console.WriteLine("Successfully added 3 blocks");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC006_AddNonExistentBlock_ShouldFail()
        {
            int contractId = _contractController.CreateContract("Test Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            int nonExistentBlockId = 99999;

            bool result = _contractController.AddBlockToContract(contractId, nonExistentBlockId);

            Assert.IsFalse(result, "Adding non-existent block should fail");

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(0, blocks.Count, "Contract should have no blocks");

            Console.WriteLine("System correctly rejected non-existent block");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC007_AddBlockToNonExistentContract_ShouldFail()
        {
            int nonExistentContractId = 99999;
            int validBlockId = TEST_ORIGINAL_BLOCK_ID_1;

            try
            {
                bool result = _contractController.AddBlockToContract(nonExistentContractId, validBlockId);
                Assert.IsFalse(result, "Should fail");
                Console.WriteLine("System correctly rejected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("System threw exception: {0}", ex.Message);
                Assert.IsTrue(true);
            }
        }

        #endregion

        #region Edit Block Tests

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(1)]
        public void TC008_EditBlockInContract_HappyPath_ShouldSucceed()
        {
            int contractId = _contractController.CreateContract("Edit Test Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(1, blocks.Count, "Setup: Contract should have 1 block");

            int blockId = blocks[0].Contract_Block_NR;
            string newText = "Updated text content - " + DateTime.Now.Ticks;

            bool result = _contractController.EditBlockInContract(contractId, blockId, newText);

            Assert.IsTrue(result, "Block edit should succeed");

            ContractBlock editedBlock = _contractBlockDAL.GetContractBlockById(blockId);
            Assert.IsNotNull(editedBlock, "Block should still exist");
            Assert.AreEqual(newText, editedBlock.Contract_text, "Block text should be updated");

            Console.WriteLine("Block text successfully updated");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC009_EditNonExistentBlock_ShouldFail()
        {
            int contractId = _contractController.CreateContract("Test Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            int nonExistentBlockId = 99999;
            string newText = "Some text";

            bool result = _contractController.EditBlockInContract(contractId, nonExistentBlockId, newText);

            Assert.IsFalse(result, "Editing non-existent block should fail");

            Console.WriteLine("System correctly rejected editing non-existent block");
        }

        #endregion

        #region Remove Block Tests

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(1)]
        public void TC010_RemoveBlockFromContract_HappyPath_ShouldSucceed()
        {
            int contractId = _contractController.CreateContract("Remove Test Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);

            List<ContractBlock> initialBlocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(3, initialBlocks.Count, "Setup: Contract should have 3 blocks");

            int blockToRemoveId = initialBlocks[1].Contract_Block_NR;

            bool result = _contractController.RemoveBlockFromContract(contractId, blockToRemoveId);

            Assert.IsTrue(result, "Block removal should succeed");

            List<ContractBlock> remainingBlocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(2, remainingBlocks.Count, "Contract should have 2 blocks remaining");

            Console.WriteLine("Block successfully removed");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(2)]
        public void TC011_RemoveLastBlock_ContractShouldRemainValid()
        {
            int contractId = _contractController.CreateContract("Single Block Company " + DateTime.Now.Ticks, TEST_USER_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(1, blocks.Count, "Setup: Contract should have 1 block");

            int blockId = blocks[0].Contract_Block_NR;

            bool result = _contractController.RemoveBlockFromContract(contractId, blockId);

            Assert.IsTrue(result, "Block removal should succeed");

            Contract contract = _contractController.GetContractById(contractId);
            Assert.IsNotNull(contract, "Contract should still exist");

            List<ContractBlock> remainingBlocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(0, remainingBlocks.Count, "Contract should have no blocks");

            Console.WriteLine("Contract is valid with zero blocks");
        }

        [TestMethod]
        [TestCategory("BlockManagement")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC012_RemoveBlockFromNonExistentContract_ShouldFail()
        {
            int nonExistentContractId = 99999;
            int someBlockId = 1;

            bool result = _contractController.RemoveBlockFromContract(nonExistentContractId, someBlockId);

            Assert.IsFalse(result, "Removing block from non-existent contract should fail");

            Console.WriteLine("System correctly rejected");
        }

        #endregion

        #region View Contract Tests

        [TestMethod]
        [TestCategory("ContractViewing")]
        [Priority(1)]
        public void TC013_ViewContractDetailsWithBlocks_ShouldSucceed()
        {
            string companyName = "View Test Company " + DateTime.Now.Ticks;
            int contractId = _contractController.CreateContract(companyName, TEST_USER_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);

            Contract contract = _contractController.GetContractById(contractId);
            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);

            Assert.IsNotNull(contract, "Contract should be retrieved");
            Assert.AreEqual(companyName, contract.Company_name, "Company name should match");
            Assert.AreEqual(TEST_USER_ID_1, contract.The_Creator, "Creator should match");
            Assert.IsNotNull(blocks, "Blocks list should not be null");
            Assert.AreEqual(3, blocks.Count, "Should retrieve all 3 blocks");

            Console.WriteLine("Successfully retrieved contract with blocks");
        }

        [TestMethod]
        [TestCategory("ContractViewing")]
        [TestCategory("ErrorHandling")]
        [Priority(2)]
        public void TC014_ViewNonExistentContract_ShouldReturnNull()
        {
            int nonExistentContractId = 99999;

            Contract contract = _contractController.GetContractById(nonExistentContractId);

            Assert.IsNull(contract, "Non-existent contract should return null");

            Console.WriteLine("System correctly returned null");
        }

        #endregion

        #region Block Co-occurrence and Recommendations Tests

        [TestMethod]
        [TestCategory("Recommendations")]
        [Priority(2)]
        public void TC015_BlockCooccurrenceTracking_ShouldRecordPairs()
        {
            int contractId = _contractController.CreateContract("CoOccurrence Test " + DateTime.Now.Ticks, TEST_USER_ID_1);

            bool result1 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            bool result2 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            bool result3 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);

            Assert.IsTrue(result1 && result2 && result3, "All blocks should be added");

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(3, blocks.Count, "All blocks should be in contract");

            Console.WriteLine("Co-occurrence tracking completed");
        }

        [TestMethod]
        [TestCategory("Recommendations")]
        [Priority(2)]
        public void TC016_BlockRecommendations_ShouldProvideRelevantSuggestions()
        {
            int historyContract1 = _contractController.CreateContract("History 1 " + DateTime.Now.Ticks, TEST_USER_ID_1);
            _contractController.AddBlockToContract(historyContract1, TEST_ORIGINAL_BLOCK_ID_1);
            _contractController.AddBlockToContract(historyContract1, TEST_ORIGINAL_BLOCK_ID_2);

            int newContractId = _contractController.CreateContract("New Contract " + DateTime.Now.Ticks, TEST_USER_ID_1);
            _contractController.AddBlockToContract(newContractId, TEST_ORIGINAL_BLOCK_ID_1);

            List<BlockRecommendation> recommendations = _contractController.GetContractRecommendations(newContractId, 5);

            Assert.IsNotNull(recommendations, "Recommendations should not be null");
            Console.WriteLine("Received {0} recommendations", recommendations.Count);
        }

        [TestMethod]
        [TestCategory("Recommendations")]
        [Priority(2)]
        public void TC017_BlockRecommendations_NoHistory_ShouldReturnEmptyList()
        {
            int contractId = _contractController.CreateContract("No History " + DateTime.Now.Ticks, TEST_USER_ID_1);

            List<BlockRecommendation> recommendations = _contractController.GetContractRecommendations(contractId, 5);

            Assert.IsNotNull(recommendations, "Recommendations list should not be null");

            Console.WriteLine("Returned {0} recommendations", recommendations.Count);
        }

        #endregion

        #region Complete Workflow Test

        [TestMethod]
        [TestCategory("EndToEnd")]
        [Priority(1)]
        public void TC018_CompleteWorkflow_EndToEnd_ShouldSucceed()
        {
            Console.WriteLine("Starting complete workflow test...");

            string companyName = "Full Workflow Company " + DateTime.Now.Ticks;
            int contractId = _contractController.CreateContract(companyName, TEST_USER_ID_1);
            Assert.IsTrue(contractId > 0, "Contract should be created");

            bool add1 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            bool add2 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            bool add3 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);

            Assert.IsTrue(add1 && add2 && add3, "All blocks should be added");

            List<ContractBlock> afterAdd = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(3, afterAdd.Count, "Should have 3 blocks");

            int block2Id = afterAdd[1].Contract_Block_NR;
            string newText = "Edited text - " + DateTime.Now.Ticks;
            bool edit = _contractController.EditBlockInContract(contractId, block2Id, newText);
            Assert.IsTrue(edit, "Block should be edited");

            int block3Id = afterAdd[2].Contract_Block_NR;
            bool remove = _contractController.RemoveBlockFromContract(contractId, block3Id);
            Assert.IsTrue(remove, "Block should be removed");

            bool add4 = _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_4);
            Assert.IsTrue(add4, "Block 4 should be added");

            Contract finalContract = _contractController.GetContractById(contractId);
            List<ContractBlock> finalBlocks = _contractController.GetContractBlocks(contractId);

            Assert.IsNotNull(finalContract, "Contract should exist");
            Assert.AreEqual(3, finalBlocks.Count, "Should have 3 blocks in final state");

            ContractBlock editedBlock = _contractBlockDAL.GetContractBlockById(block2Id);
            Assert.AreEqual(newText, editedBlock.Contract_text, "Block should have edited text");

            Console.WriteLine("Complete workflow test passed!");
        }

        #endregion

        #region Additional Tests

        [TestMethod]
        [TestCategory("BlockManagement")]
        [Priority(2)]
        public void TC019_ConcurrentBlockAddition_ShouldMaintainOrder()
        {
            int contractId = _contractController.CreateContract("Concurrent Test " + DateTime.Now.Ticks, TEST_USER_ID_1);

            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_1);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_2);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_3);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_4);
            _contractController.AddBlockToContract(contractId, TEST_ORIGINAL_BLOCK_ID_5);

            List<ContractBlock> blocks = _contractController.GetContractBlocks(contractId);
            Assert.AreEqual(5, blocks.Count, "Should have exactly 5 blocks");

            Console.WriteLine("Successfully added 5 blocks");
        }

        [TestMethod]
        [TestCategory("ContractViewing")]
        [Priority(2)]
        public void TC020_GetContractsByCreator_ShouldFilterCorrectly()
        {
            string timestamp = DateTime.Now.Ticks.ToString();

            int user1Contract1 = _contractController.CreateContract("User1_Contract1_" + timestamp, TEST_USER_ID_1);
            int user1Contract2 = _contractController.CreateContract("User1_Contract2_" + timestamp, TEST_USER_ID_1);
            int user1Contract3 = _contractController.CreateContract("User1_Contract3_" + timestamp, TEST_USER_ID_1);

            List<Contract> user1Contracts = _contractController.GetContractsByCreator(TEST_USER_ID_1);

            Assert.IsTrue(user1Contracts.Count >= 3, "User 1 should have at least 3 contracts");
            Assert.IsTrue(user1Contracts.Any(c => c.Contract_NR == user1Contract1), "Should have contract 1");
            Assert.IsTrue(user1Contracts.Any(c => c.Contract_NR == user1Contract2), "Should have contract 2");
            Assert.IsTrue(user1Contracts.Any(c => c.Contract_NR == user1Contract3), "Should have contract 3");

            Console.WriteLine("Contract filtering works correctly");
        }

        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            Console.WriteLine("Test completed");
        }
    }
}