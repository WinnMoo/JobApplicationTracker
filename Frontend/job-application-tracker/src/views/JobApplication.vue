<template>
  <div class="JobApplication">
    <v-dialog v-model="addDialog" persistent max-width="600px" ref="dialog">
      <template v-slot:activator="{ on }">
        <v-btn class="mx-2" fab dark color="indigo" fixed left bottom v-on="on">
          <v-icon dark>mdi-plus</v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-form ref="form">
          <v-card-title>
            <span class="headline">Add New Job Application</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" sm="6" md="6">
                  <v-text-field
                    :rules="[rules.required]"
                    label="Company Name*"
                    v-model="companyName"
                    hint="E.g. Microsoft"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="6">
                  <v-text-field
                    :rules="[rules.required]"
                    label="Job Title*"
                    v-model="jobTitle"
                    hint="E.g. Software Engineer"
                    required
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" sm="12" md="12">
                  <v-textarea
                    :rules="[rules.required]"
                    label="Description*"
                    v-model="description"
                    required
                  ></v-textarea>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="closeAddDialog()">Close</v-btn>
            <v-btn color="blue darken-1" text @click="addJobApplication()">Save</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
    <v-btn class="mx-2" fab dark color="indigo" fixed right bottom @click="top">
      <v-icon dark>mdi-chevron-up</v-icon>
    </v-btn>
    <v-container>
      <v-row v-for="(jobApplication, index) in jobApplications" :key="index">
        <v-col>
          <JobApplicationCard v-for="jobApplication in jobApplications"
                              v-bind:key="index"
                              v-bind:jobApplicaion='jobApplication'></JobApplicationCard>
        </v-col>
      </v-row>
      <v-dialog v-model="deleteDialog" width="500">
        <v-card>
          <v-card-title>Are you sure you want to delete this?</v-card-title>
          <v-card-text>Deleted applications cannot be restored.</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              text
              @click="deleteDialog = false; deleteJobApplication(indexToDelete)"
            >Yes</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="updateDialog" persistent max-width="600px" ref="dialog">
        <v-card>
          <v-form ref="form">
            <v-card-title>
              <span class="headline">Update Job Application</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field
                      :rules="[rules.required]"
                      label="Company Name*"
                      v-model="companyName"
                      hint="E.g. Microsoft"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field
                      :rules="[rules.required]"
                      label="Job Title*"
                      v-model="jobTitle"
                      hint="E.g. Software Engineer"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field
                      :rules="[rules.required]"
                      label="Description*"
                      v-model="description"
                      hint="example of persistent helper text"
                      required
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
              <small>*indicates required field</small>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeUpdateDialog()">Close</v-btn>
              <v-btn color="blue darken-1" text @click="updateJobApplication(indexToUpdate)">Save</v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-dialog>
    </v-container>
  </div>
</template>

<script>
import JobApplicationCard from '@/components/JobApplicationCard.vue'
export default {
  components: {
    JobApplicationCard
  },
  name: "JobApplication",
  data() {
    return {
      addDialog: false,
      deleteDialog: false,
      updateDialog: false,
      indexToDelete: null,
      indexToUpdate: null,
      companyName: null,
      jobTitle: null,
      description: null,
      location: null,
      rules: {
        required: value => !!value || "Required."
      },
      jobApplications: [
        {
          company: "Microsoft",
          location: "Long Beach, CA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "1",
          dateApplied: "12/12/12"
        },
        {
          company: "Apple",
          location: "Seattle, WA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "0",
          dateApplied: "12/12/12"
        },
        {
          company: "Google",
          location: "Irvine, CA",
          jobTitle: "Software Engineer",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
          status: "-1",
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
    deleteJobApplication: function(index) {
      this.jobApplications.splice(index, 1);
    },
    addJobApplication: function() {
      if (
        this.companyName != null &&
        this.jobTitle != null &&
        this.description != null
      ) {
        var newJobApp = {
          company: this.companyName,
          jobTitle: this.jobTitle,
          description: this.description
        };
        this.addDialog = false;
        this.jobApplications.push(newJobApp);
        this.$refs.form.reset();
      } else {
        this.companyname = null;
        this.jobTitle = null;
        this.description = null;
      }
    },
    updateJobApplication: function(indexToUpdate) {
      if (
        this.companyName != null &&
        this.jobTitle != null &&
        this.description != null
      ) {
        var updatedJobApp = {
          company: this.companyName,
          jobTitle: this.jobTitle,
          description: this.description
        };
        this.updateDialog = false;
        this.jobApplications.splice(indexToUpdate, 1, updatedJobApp);
        console.log(updatedJobApp);
        this.indexToUpdate = null;
        this.$refs.form.reset();
      } else {
        this.companyname = null;
        this.jobTitle = null;
        this.description = null;
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
