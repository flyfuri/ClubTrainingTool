using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class DataAccess
    {
        public List<Training> getLatestDate()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
               return connection.Query<Training>("dbo.Trainings_getLatest").ToList();
            }
        }

        public List<Training> getTrainingByYEAR(int actYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Training>("dbo.Training_GetByYEAR @actYear", new { actYear = actYear }).ToList();
            }
        }

        public List<Training> getTrainingByID(int trainingID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Training>("dbo.Training_GetByID @TrainingID", new { TrainingID = trainingID}).ToList();
            }
        }

        public List<Training> getTrainingByDate(DateTime trainingDate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Training>("dbo.Training_GetByTrainingDate @TrainingDate", new { TrainingDate = trainingDate }).ToList();
            }
        }

        public List<Training> getTrainingsOrderedByDate(int orderDESC)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Training>("dbo.Trainings_orderdByDate @OrderDESC", new { OrderDESC = orderDESC }).ToList();
            }
        }

        public void updateTraining(List<Training> listTrainingsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Training_update @TrainingID, @TrainingDate, @CashAtBegin, @CashToACBEO, @CashAtEnd, @Remarks, @Leiter1_ID, @Leiter2_ID", listTrainingsToUpdate);
            }
        }

        public void addTrainings (List<Training> listTrainingsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Trainings_add @TrainingDate, @CashAtBegin, @CashToACBEO, @CashAtEnd, @Remarks, @Leiter1_ID, @Leiter2_ID", listTrainingsToAdd);
            }
        }

        //Pilots**********************************************************************************
        public List<Pilot> getPilotByID(int pilot_ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Pilot>("dbo.Pilots_getByID @Pilot_ID", new { Pilot_ID = pilot_ID }).ToList();
            }
        }


        public List<Pilot> getPilots(int colNumToOrder)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Pilot>("dbo.Pilots_getAll @ColNumToOrder", new { ColNumToOrder = colNumToOrder }).ToList();
            }
        }

        public List<Pilot> getActivPilots(int colNumToOrder)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Pilot>("dbo.Pilots_getAllActiv @ColNumToOrder", new { ColNumToOrder = colNumToOrder }).ToList();
            }
        }

        public void addPilot(List<Pilot> listPilotsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                //List<Pilot> pilotToAdd = new List<Pilot>();
                //pilotToAdd.Add(new Pilot { LicensNr = licensNr, FirstName = firstName, LastName = lastName });
                connection.Execute("dbo.Pilots_add @LicensNr, @FirstName, @LastName, @Disactivated", listPilotsToAdd);
            }
        }


        public void updatePilot(List<Pilot> listPilotsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Pilots_update @Pilot_ID, @LicensNr, @FirstName, @LastName, @Disactivated", listPilotsToUpdate);
            }
        }


        public void deletePilot(List<Pilot> listPilotsToDelete)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Pilots_delete @Pilot_ID", listPilotsToDelete);
            }
        }

//Participant*****************************************************3        

        public List<Participant> getParticipants(int trainingID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Participant>("dbo.ParticipantsByTrainingID @TrainingID", new { TrainingID = trainingID }).ToList();
            }
        }

        public void addParticipant(List<Participant> listParticipantToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Participants_add @TrainingID, @PilotID", listParticipantToAdd);
            }
        }

        public void deleteParticipant(List<Participant> listParticipantsToDelete)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Participants_delete @ParticipantID", listParticipantsToDelete);
            }
        }

        public void deleteFromAllTblsParticipant(List<Participant> listParticipantsToDelete)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Participants_deleteFromAllTables @ParticipantID", listParticipantsToDelete);
            }
        }

        public List<Turn> getTurnsByTrainIDParticipID(int trainingID, int participantID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TrainingID", trainingID);
                parameters.Add("@ParticipantID", participantID);
                return connection.Query<Turn>("dbo.TurnsByTrainingIDParticipantID @TrainingID, @ParticipantID", parameters).ToList();
            }
        }

        public List<Turn> getTurnByTurnID(int turnID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Turn>("dbo.Turn_getByTurnID @TurnID", new { turnID = turnID }).ToList();
            }
        }
  

        public void addTurn(List<Turn> listTurnsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                 connection.Execute("dbo.Turns_add @TrainingID, @ParticipantID, @TurnNr, @Flight", listTurnsToAdd);
            }
        }


        public void updateTurns(List<Turn> listTurnsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Turns_update @TurnID, @TrainingID, @ParticipantID, @TurnNr, @Flight", listTurnsToUpdate);
            }
        }

        //DayPilotCosts*****************************************************

        public List<DayPilotCost> getDayPilotCostsByTrainIDParticipID(int trainingID, int participantID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TrainingID", trainingID);
                parameters.Add("@ParticipantID", participantID);
                return connection.Query<DayPilotCost>("dbo.DayPilotCostsByTrainingIDParticipantID @TrainingID, @ParticipantID", parameters).ToList();
            }
        }

        public List<DayPilotCost> getDayPilotCostsByTrainID(int trainingID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
               return connection.Query<DayPilotCost>("dbo.DayPilotCostsByTrainingID @TrainingID", new { TrainingID = trainingID }).ToList();
            }
        }

        /*public List<Turn> getDayPilotCostByID(int turnID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Turn>("dbo.DayPilotCosts_getByID @DayPilotCostID", new { DayPilotCostID = turnID }).ToList();
            }
        }*/

        public void addDayPilotCost (List<DayPilotCost> listDayPilotCostsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.DayPilotCosts_add @TrainingID, @ParticipantID, @CostFlights, @CostOtherServices, @CostBuy, @PayedAmount, @PayedFlag, @PayedTwint, @PayedTwintReference", listDayPilotCostsToAdd);
            }
        }


        public void updateDayPilotCosts(List<DayPilotCost> listDayPilotCostsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.DayPilotCosts_update @DayPilotCostID, @TrainingID, @ParticipantID, @CostFlights, @CostOtherServices, @CostBuy, @PayedAmount, @PayedFlag, @PayedTwint, @PayedTwintReference", listDayPilotCostsToUpdate);
            }
        }

        //***************************************************************************************
        //DayPilotBuys*****************************************************

        public List<DayPilotBuy> getDayPilotBuyByTrainIDParticipID(int trainingID, int participantID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TrainingID", trainingID);
                parameters.Add("@ParticipantID", participantID);
                return connection.Query<DayPilotBuy>("dbo.DayPilotBuyByTrainingIDParticipantID @TrainingID, @ParticipantID", parameters).ToList();
            }
        }

        public void addDayPilotBuy(List<DayPilotBuy> listDayPilotBuyToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.DayPilotBuy_add @TrainingID, @ParticipantID, @LifewestBuy, @LifewestRent, @YearFee, @DayFee, @AxalpWeekFee, @Others, @Remarks", listDayPilotBuyToAdd);
            }
        }


        public void updateDayPilotBuy(List<DayPilotBuy> listDayPilotBuyToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.DayPilotBuy_update @DayPilotBuyID, @TrainingID, @ParticipantID, @LifewestBuy, @LifewestRent, @YearFee, @DayFee, @AxalpWeekFee, @Others, @Remarks", listDayPilotBuyToUpdate);
            }
        }


        //***************************************************************************************
        //Abos*****************************************************

        public List<AboFlight> getAboFlightNrsDayPilot(int pilotID, int trainingsID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@TrainingID", trainingsID);
                return connection.Query<AboFlight>("dbo.AboFlight_getAboNrsOfDayPilot @PilotID, @TrainingID", parameters).ToList();
            }
        }

        public List<AboFlight> getAboFlightUsablePilotBoughtThisTraining(int pilotID, int trainingsID, int Abo_NrInYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@TrainingID", trainingsID);
                parameters.Add("@Abo_NrInYEAR", Abo_NrInYear);
                return connection.Query<AboFlight>("dbo.AboFlight_getUsableOfPilotBoughtThisTraining @PilotID, @TrainingID, @Abo_NrInYEAR", parameters).ToList();
            }
        }

        public List<AboFlight> getAboFlightPayedWithDayPilot(int pilotID, int trainingsID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@TrainingID", trainingsID);
                return connection.Query<AboFlight>("dbo.AboFlight_getAboFlightsPayedWithDayPilotID @PilotID, @TrainingID", parameters).ToList();
            }
        }

        public List<AboFlight> getAboFlightPayedBackWithDayPilot(int pilotID, int trainingsID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@TrainingID", trainingsID);
                return connection.Query<AboFlight>("dbo.AboFlight_getAboFlightsPayedBackWithDayPilotID @PilotID, @TrainingID", parameters).ToList();
            }
        }

        public List<AboFlight> getUsableAboFlightsByPilotID(int pilotID, int actYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@actYear", actYear);
                return connection.Query<AboFlight>("dbo.AboFlights_getAllUsableByPilotID @PilotID, @actYear", parameters).ToList();
            }
        }

        public List<AboFlight> getAllAboFlightsByPilotIDinValidPeriod(int pilotID, int actYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PilotID", pilotID);
                parameters.Add("@actYear", actYear);
                return connection.Query<AboFlight>("dbo.AboFlights_getAllByPilotIDinValidPeriod @PilotID, @actYear", parameters).ToList();
            }
        }

        public List<AboFlight> getAllAboFlightsCurrentYear(int actYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<AboFlight>("dbo.AboFlights_getAllOfCurrentYear @actYear", new { actYear = actYear }).ToList();
            }
        }

        public List<UnusedAbosToExport> getAllUnusedAboFlightsCurrent2Years(int actYear)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<UnusedAbosToExport>("dbo.getAllAbosFlightsUnused_sortedByName @actYear", new { actYear = actYear }).ToList();
            }
        }
        
        public void addAboFlights(List<AboFlight> listAboFlightsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.AboFlights_add @PilotID, @Abo_YEAR, @Abo_NrInYEAR, @FlightNrOnAbo, @DateBought, @DateFlightPayedWith, @Comment, @SellerID", listAboFlightsToAdd);
            }
        }

        public void updateAboFlights(List<AboFlight> listAboFlightsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.[AboFlights_update  @AboFlight_ID, @PilotID, @Abo_YEAR, @Abo_NrInYEAR, @FlightNrOnAbo, @DateBought, @DateFlightPayedWith, @Comment, @SellerID", listAboFlightsToUpdate);
            }
        }

        public void updateAboFlightsDatePayedWith(List<AboFlight> listAboFlightsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.AboFlights_updateDateFlightPayedWith  @AboFlight_ID, @DateFlightPayedWith", listAboFlightsToUpdate);
            }
        }

        public void deleteAboFlights(List<AboFlight> listAboFlightsToDelete)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.AboFlights_deleteByID  @AboFlight_ID", listAboFlightsToDelete);
            }
        }

        //***************************************************************************************
        //TrainingCosts*****************************************************

        public List<TrainingCost> getTrainingCost_allcurrentYear()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<TrainingCost>("dbo.TrainingCosts_getAllCurrentYear ").ToList();
            }
        }

        public List<TrainingCost> getTrainingCostByID(int trainingCostID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<TrainingCost>("dbo.TrainingCosts_getByID @TrainingCostID", new { TrainingCostID = trainingCostID }).ToList();
            }
        }

        public List<TrainingCost> getTrainingCostByDate(DateTime trainingDate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<TrainingCost>("dbo.TrainingCosts_getByDate @TrainingDate", new { TrainingDate = trainingDate.AddHours(12) }).ToList();  ///provisorisch .AddHours(12)
            }
        }

        public List<TrainingCost> getTrainingCostByDateAndBelegNr(DateTime trainingDate, string belegNr)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TrainingDate", trainingDate);
                parameters.Add("@BelegNr", belegNr);
                return connection.Query<TrainingCost>("dbo.TrainingCosts_getByDateAndBelegNr @TrainingDate, @BelegNr", parameters).ToList();
            }
        }

        public void addTrainingCosts(List<TrainingCost> listTrainingCostsToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.TrainingCosts_add @TrainingDate, @Betrag, @Kommentar, @BelegNr, @BelegPhotoName", listTrainingCostsToAdd);
            }
        }


        public void updateTrainingCosts(List<TrainingCost> listTrainingCostsToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.TrainingCosts_update @TrainingCostID, @TrainingDate, @Betrag, @Kommentar, @BelegNr, @BelegPhotoName", listTrainingCostsToUpdate);
            }
        }

        /*public List<Abo> geAboByAboID(int aboID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Abo>("dbo.getAbo_byAboID @Abo_ID", new { Abo_ID = aboID }).ToList();
            }
        }

        public List<Abo> getAbosByPilotID(int pilotID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Abo>("dbo.getAbos_byPilotID @PilotID", new { PilotID = pilotID }).ToList();
            }
        }

        public List<Abo> getUsableAbosByPilotID(int pilotID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Abo>("dbo.getAboUsable_byPilotID @PilotID", new { PilotID = pilotID }).ToList();
            }
        }

        public List<Abo> getAllAbosCurrentYear()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                return connection.Query<Abo>("dbo.getAllAbosCurrentYear").ToList();
            }
        }

        public void addAbos(List<Abo> listAbosToAdd)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Abos_add @PilotID, @Abo_Nr", listAbosToAdd);
            }
        }

        public void updateAbos(List<Abo> listAbosToUpdate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DBHelper.CnnVal("TrainingsRapportConnectionString")))
            {
                connection.Execute("dbo.Abos_update @Abo_ID, @PilotID, @Abo_Nr, @DateBought, @DateFlight1, @DateFlight2, @DateFlight3, @DateFlight4, @DateFlight5, @DateFlight6, @DateFlight7, @DateFlight8, @DateFlight9, @DateFlight10", listAbosToUpdate);
            }
        }*/
    }
}
