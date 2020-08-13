<template>
  <div class="JobApplication">
    <AddJobApplicationDialog v:on @addJobApplication="addJobApplication"></AddJobApplicationDialog>
    <v-btn class="mx-2" fab dark color="indigo" fixed right bottom @click="top">
      <v-icon dark>mdi-chevron-up</v-icon>
    </v-btn>
    <v-container>
      <JobApplicationCard v-for="jobApplication in jobApplications" 
                          v-bind:key="jobApplication.id" 
                          v-bind:jobApplication = "jobApplication"
                          v:on @openUpdateDialog="openUpdateDialog"></JobApplicationCard>
      <DeleteJobApplicationDialog @deleteJobApplication="deleteJobApplication"></DeleteJobApplicationDialog>
      <UpdateJobApplicationDialog v-bind:updateDialog = "this.updateDialog"
                                  v:on @updateJobApplication = "updateJobApplication"></UpdateJobApplicationDialog>
    </v-container>
  </div>
</template>

<script>
import AddJobApplicationDialog from '@/components/JobApplicationDialogs/AddJobApplicationDialog.vue'
import DeleteJobApplicationDialog from '@/components/JobApplicationDialogs/DeleteJobApplicationDialog.vue'
import UpdateJobApplicationDialog from '@/components/JobApplicationDialogs/UpdateJobApplicationDialog.vue'
import JobApplicationCard from '@/components/JobApplicationCard.vue'

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
      addDialog: false,
      deleteDialog: false,
      updateDialog: false,
      indexToDelete: null,
      indexToUpdate: null,
      jobApplications: [
        {
          id: 0,
          company: "Microsoft",
          location: "Long Beach, CA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "1",
          dateApplied: "12/12/12"
        },
        {
          id: 1,
          company: "Apple",
          location: "Seattle, WA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "0",
          dateApplied: "12/12/12"
        },
        {
          id: 2,
          company: "Google",
          location: "Irvine, CA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "2",
          dateApplied: "12/12/12"
        },
        {
          id: 3,
          company: "Belkin",
          location: "Irvine, CA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "3",
          dateApplied: "12/12/12"
        }
      ]
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
    deleteJobApplication: function() {
      this.deleteDialog = false
    },
    addJobApplication: function(companyName, jobTitle, description) {
      if (
        companyName != null &&
        jobTitle != null &&
        description != null
      ) {
        var newJobApp = {
          id: this.jobApplications.length + 1,
          company: companyName,
          jobTitle: jobTitle,
          description: description
        };
        this.addDialog = false;
        this.jobApplications.push(newJobApp);
         // Change this push to use an api call to add job application 
        this.$forceUpdate;
      }
    },
    openUpdateDialog: function(openDialog){
      this.updateDialog = openDialog;
      console.log(this.updateDialog);
    },
    updateJobApplication: function(companyName, jobTitle, description, dialogCondition) {
      if(dialogCondition){
        if (
        companyName != null &&
        jobTitle != null &&
        description != null
      ) {
        var updatedJobApp = {
          company: companyName,
          jobTitle: jobTitle,
          description: description
        };
        this.updateDialog = false;
        this.jobApplications.splice(0, 1, updatedJobApp);
        this.$forceUpdate;
      }
      } else {
        this.updateDialog = false;
      }
      
    },
    closeAddDialog: function() {
      this.$refs.form.reset();
      this.addDialog = false;
    },
    closeUpdateDialog: function() {
      this.updateDialog = false;
      this.companyname = null;
      this.jobTitle = null;
      this.description = null;
      this.$refs.form.reset();
    }
  }
};
</script>
