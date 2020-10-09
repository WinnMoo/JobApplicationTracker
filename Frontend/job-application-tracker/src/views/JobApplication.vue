<template>
  <div class="JobApplication">
    <v-alert v-model="errorPopup" dismissible type="error">{{
      errorMessage
    }}</v-alert>
    <AddJobApplicationDialog
      v-bind:viewTypeProp="this.viewType"
      v:on
      @addJobApplication="addJobApplication"
    ></AddJobApplicationDialog>
    <v-btn class="mx-2" fab dark color="indigo" fixed right bottom @click="top">
      <v-icon dark>mdi-chevron-up</v-icon>
    </v-btn>
    <v-container>
      <JobApplicationToolbar
        v:on
        @sortJobApplications="sortJobApplications"
        @changeViewType="changeViewType"
      ></JobApplicationToolbar>
      <div v-if="viewType == 1">
        <JobApplicationCard
          v-for="jobApplication in jobApplications"
          v-bind:key="jobApplication.id"
          v-bind:jobApplication="jobApplication"
          v:on
          @openUpdateDialog="openUpdateDialog"
          @updateStatus="updateStatus"
          @openDeleteDialog="openDeleteDialog"
          @updateLocation="updateLocation"
        ></JobApplicationCard>
      </div>
      <div v-else-if="viewType == 2">
        <div class="sticky">
          <JobApplicationDenseInfo></JobApplicationDenseInfo>
        </div>
        
        <JobApplicationCard
          v-for="jobApplication in jobApplications"
          v-bind:key="jobApplication.id"
          v-bind:jobApplication="jobApplication"
          v:on
          @openUpdateDialog="openUpdateDialog"
          @updateStatus="updateStatus"
          @openDeleteDialog="openDeleteDialog"
          @updateLocation="updateLocation"
        ></JobApplicationCard>
      </div>

      <DeleteJobApplicationDialog
        v-bind:deleteDialog="this.deleteDialog"
        v-bind:idToDelete="this.idToModify"
        v:on
        @deleteJobApplication="deleteJobApplication"
      ></DeleteJobApplicationDialog>
      <UpdateJobApplicationDialog
        v-bind:updateDialog="this.updateDialog"
        v-bind:idToUpdate="this.idToModify"
        v-bind:jobTitle="this.jobTitleToUpdate"
        v-bind:companyName="this.companyNameToUpdate"
        v-bind:description="this.descriptionToUpdate"
        v:on
        @updateJobApplication="updateJobApplicationInfo"
      ></UpdateJobApplicationDialog>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
import {
  compareJobTitle,
  compareCompanyName,
  compareDateApplied,
  compareStatusAscending,
  compareStatusDescending
} from "@/services/ComparerService";
import JobApplicationDenseInfo from "@/components/JobApplicationComponents/JobApplicationDenseInfo.vue";
import JobApplicationCard from "@/components/JobApplicationCard.vue";
import JobApplicationToolbar from "@/components/JobApplicationComponents/JobApplicationToolbar.vue";
import AddJobApplicationDialog from "@/components/JobApplicationComponents/AddJobApplicationDialog.vue";
import DeleteJobApplicationDialog from "@/components/JobApplicationComponents/DeleteJobApplicationDialog.vue";
import UpdateJobApplicationDialog from "@/components/JobApplicationComponents/UpdateJobApplicationDialog.vue";

export default {
  components: {
    JobApplicationCard,
    JobApplicationToolbar,
    JobApplicationDenseInfo,
    AddJobApplicationDialog,
    DeleteJobApplicationDialog,
    UpdateJobApplicationDialog
  },
  name: "JobApplication",
  data() {
    return {
      viewType: 1,
      errorPopup: false,
      errorMessage: null,
      addDialog: false,
      deleteDialog: false,
      updateDialog: false,
      idToModify: null,
      jobTitleToUpdate: null,
      companyNameToUpdate: null,
      descriptionToUpdate: null,
      JobApplication: {
        jobApplicationId: null,
        companyName: null,
        jobtTitle: null,
        description: null,
        status: null,
        city: null,
        state: null,
        urlToJobPosting: null,
        dateApplied: null,
        userEmail: null
      },
      jobApplications: null
    };
  },
  methods: {
    top: function() {
      window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth"
      });
    },
    fetchJobApplications: function() {
      axios({
        method: "GET",
        url: `${apiURL}/jobapp/` + "get",
        params: {
          StartIndex: 0,
          NumOfItemsToGet: -1,
          EmailAddress: this.$store.getters.emailAddress
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          this.jobApplications = response.data;
          this.$forceUpdate;
        })
        .catch(e => {
          this.ErrorMessage = e.response.data;
        });
    },
    openDeleteDialog: function(openDialog, jobApplicationId) {
      this.deleteDialog = openDialog;
      this.idToModify = jobApplicationId;
    },
    openUpdateDialog: function(openDialog, jobApplicationId) {
      this.updateDialog = openDialog;
      this.idToModify = jobApplicationId;
      let jobApplicationIndex = this.jobApplications.findIndex(
        element => element.jobApplicationId == jobApplicationId
      );
      this.jobTitleToUpdate = this.jobApplications[
        jobApplicationIndex
      ].jobTitle;
      this.companyNameToUpdate = this.jobApplications[
        jobApplicationIndex
      ].companyName;
      this.descriptionToUpdate = this.jobApplications[
        jobApplicationIndex
      ].description;
    },
    addJobApplication: function(
      companyName,
      jobTitle,
      description,
      jobPostingUrl,
      city,
      state
    ) {
      if (companyName != null && jobTitle != null) {
        axios({
          method: "POST",
          url: `${apiURL}/jobapp/` + "add",
          data: {
            JobAppId: null,
            CompanyName: companyName,
            JobTitle: jobTitle,
            Description: description,
            Status: 0,
            City: city,
            State: state,
            URLToJobPosting: jobPostingUrl,
            DateApplied: Date.UTC.now,
            UserEmail: this.$store.getters.emailAddress
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.jobApplications.push(response.data);
            this.$forceUpdate;
          })
          .catch(e => {
            this.ErrorMessage = e.response.data;
          });
        this.addDialog = false;
        this.$forceUpdate;
      }
    },
    deleteJobApplication: function(jobApplicationId, deleteCondition) {
      if (deleteCondition) {
        axios({
          method: "DELETE",
          url: `${apiURL}/jobapp/` + "delete",
          data: {
            jobApplicationId
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            let jobApplicationIndex = this.jobApplications.findIndex(
              element => element.jobApplicationId == jobApplicationId
            );
            let updatedJobApplications = this.jobApplications;
            updatedJobApplications.splice(jobApplicationIndex, 1);
            this.jobApplications = updatedJobApplications;
            this.$forceUpdate;
            return response;
          })
          .catch(e => {
            this.ErrorMessage = e.response.data;
            this.errorPopup = true;
          })
          .finally(() => {
            this.deleteDialog = false;
          });
      } else {
        this.deleteDialog = false;
      }
    },
    updateJobApplicationInfo: function(
      companyName,
      jobTitle,
      description,
      jobApplicationId,
      dialogCondition
    ) {
      if (dialogCondition) {
        if (companyName != null && jobTitle != null) {
          this.updateDialog = false;
          let jobApplicationIndex = this.jobApplications.findIndex(
            element => element.jobApplicationId == jobApplicationId
          );
          this.JobApplication = this.jobApplications[jobApplicationIndex];
          this.JobApplication.companyName = companyName;
          this.JobApplication.jobTitle = jobTitle;
          this.JobApplication.description = description;
          this.updateJobApplication(this.JobApplication, jobApplicationIndex);
        }
      }
      this.updateDialog = false;
      this.companyNameToUpdate = null;
      this.jobTitleToUpdate = null;
      this.descriptionToUpdate = null;
    },
    updateStatus: function(status, jobApplicationId) {
      let jobApplicationIndex = this.jobApplications.findIndex(
        element => element.jobApplicationId == jobApplicationId
      );
      this.JobApplication = this.jobApplications[jobApplicationIndex];
      this.JobApplication.status = status;
      this.updateJobApplication(this.JobApplication, jobApplicationIndex);
    },
    updateLocation: function(city, state, jobApplicationId) {
      let jobApplicationIndex = this.jobApplications.findIndex(
        element => element.jobApplicationId == jobApplicationId
      );
      this.JobApplication = this.jobApplications[jobApplicationIndex];
      this.JobApplication.city = city;
      this.JobApplication.state = state;
      this.updateJobApplication(this.JobApplication, jobApplicationIndex);
    },
    updateJobApplication: function(JobApplication, jobApplicationIndex) {
      axios({
        method: "PUT",
        url: `${apiURL}/jobapp/` + "update",
        data: {
          JobAppId: JobApplication.jobApplicationId,
          CompanyName: JobApplication.companyName,
          JobTitle: JobApplication.jobTitle,
          Description: JobApplication.description,
          Status: JobApplication.status,
          City: JobApplication.city,
          State: JobApplication.state,
          URLToJobPosting: JobApplication.jobPostingURL,
          DateApplied: JobApplication.dateApplied,
          UserEmail: this.$store.getters.emailAddress
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          this.$set(this.jobApplications, jobApplicationIndex, response.data);
        })
        .catch(e => {
          this.ErrorMessage = e.response.data;
        })
        .finally(
          (this.companyNameToUpdate = null),
          (this.jobTitleToUpdate = null),
          (this.descriptionToUpdate = null)
        );
    },
    sortJobApplications(sortType) {
      if (sortType == "Date Applied") {
        this.jobApplications = this.jobApplications.sort(compareDateApplied);
      } else if (sortType == "Status (Ascending)") {
        this.jobApplications = this.jobApplications.sort(
          compareStatusAscending
        );
      } else if (sortType == "Status (Descending)") {
        this.jobApplications = this.jobApplications.sort(
          compareStatusDescending
        );
      } else if (sortType == "Job Title") {
        this.jobApplications = this.jobApplications.sort(compareJobTitle);
      } else if (sortType == "Company Name") {
        this.jobApplications = this.jobApplications.sort(compareCompanyName);
      }
      this.$forceUpdate;
    },
    changeViewType(viewType) {
      this.viewType = viewType;
      this.$forceUpdate;
    }
  },
  created: function() {
    this.fetchJobApplications();
  }
};
</script>
<style>
#JobApplicationCard {
  height: 50vh;
}
.sticky {
  position: sticky;
  position: -webkit-sticky;
  top: 60px; 
  display: block;
  z-index: 100;
  padding-top: 50
}
</style>
