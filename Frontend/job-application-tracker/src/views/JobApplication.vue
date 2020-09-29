<template>
  <div class="JobApplication">
    <v-alert v-model="errorPopup" dismissible type="error">{{
      errorMessage
    }}</v-alert>
    <AddJobApplicationDialog
      v:on
      @addJobApplication="addJobApplication"
    ></AddJobApplicationDialog>
    <v-btn class="mx-2" fab dark color="indigo" fixed right bottom @click="top">
      <v-icon dark>mdi-chevron-up</v-icon>
    </v-btn>
    <v-container>
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
      <DeleteJobApplicationDialog
        v-bind:deleteDialog="this.deleteDialog"
        v-bind:idToDelete="this.idToModify"
        v:on
        @deleteJobApplication="deleteJobApplication"
      ></DeleteJobApplicationDialog>
      <UpdateJobApplicationDialog
        v-bind:updateDialog="this.updateDialog"
        v-bind:idToUpdate="this.idToModify"
        v:on
        @updateJobApplication="updateJobApplicationInfo"
      ></UpdateJobApplicationDialog>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
import JobApplicationCard from "@/components/JobApplicationCard.vue";
import AddJobApplicationDialog from "@/components/JobApplicationDialogs/AddJobApplicationDialog.vue";
import DeleteJobApplicationDialog from "@/components/JobApplicationDialogs/DeleteJobApplicationDialog.vue";
import UpdateJobApplicationDialog from "@/components/JobApplicationDialogs/UpdateJobApplicationDialog.vue";

export default {
  components: {
    AddJobApplicationDialog,
    JobApplicationCard,
    DeleteJobApplicationDialog,
    UpdateJobApplicationDialog
  },
  name: "JobApplication",
  data() {
    return {
      errorPopup: false,
      errorMessage: null,
      addDialog: false,
      deleteDialog: false,
      updateDialog: false,
      idToModify: null,
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
      jobApplications: []
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
        if (companyName != null && jobTitle != null && description != null) {
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
        });
    }
  },
  created: function() {
    this.fetchJobApplications();
  }
};
</script>
