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
            <v-btn color="blue darken-1" text @click="closeAddDialog()">Close</v-btn>
            <v-btn color="blue darken-1" text @click="addJobApplication()">Save</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
    <v-btn class="mx-2" fab dark color="indigo" fixed right bottom @click="top">
      <v-icon dark>mdi-chevron-up</v-icon>
    </v-btn>
    <v-row v-for="(jobApplication, index) in jobApplications" :key="index">
      <v-col>
        <v-card class="mx-auto" max-width="600">
          <v-card-text>
            <p class="display-1 text--primary">
              {{ jobApplication.company }}
            </p>
            <p>{{ jobApplication.jobTitle }}</p>
            <div class="text--primary">
              {{ jobApplication.description }}
            </div>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn small color="primary" @click="updateDialog = true;
              indexToUpdate = index;
              companyName = jobApplications[index].company;
              jobTitle = jobApplications[index].jobTitle;
              description = jobApplications[index].description"> 
              Edit 
            </v-btn>
            <v-btn small color="error" @click="deleteDialog = true; indexToDelete = index">
              Delete
            </v-btn>
          </v-card-actions>
          <v-expansion-panels>
            <v-expansion-panel>
              <v-expansion-panel-header>
                <template v-slot:default="{ open }">
                  <v-row no-gutters>
                    <v-col cols="4">Trip name</v-col>
                    <v-col
                      cols="8"
                      class="text--secondary"
                    >
                      <v-fade-transition leave-absolute>
                        <span
                          v-if="open"
                          key="0"
                        >
                          Enter a name for the trip
                        </span>
                        <span
                          v-else
                          key="1"
                        >
                          {{ location }}
                        </span>
                      </v-fade-transition>
                    </v-col>
                  </v-row>
                </template>
              </v-expansion-panel-header>
              <v-expansion-panel-content>
                <v-text-field
                  v-model="location"
                  placeholder="Caribbean Cruise"
                ></v-text-field>
              </v-expansion-panel-content>
            </v-expansion-panel>
          </v-expansion-panels>
        </v-card>
      </v-col>
    </v-row>
    <v-dialog v-model="deleteDialog" width="500">
      <v-card>
        <v-card-title>
          Are you sure you want to delete this?
        </v-card-title>
        <v-card-text>
          Deleted applications cannot be restored.
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="deleteDialog = false; deleteJobApplication(indexToDelete)">
            Yes
          </v-btn>
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
  </div>
</template>

<script>
export default {
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
          jobTitle: "Software Engineer",
          description: "program stuff"
        },
        {
          company: "Apple",
          jobTitle: "Software Engineer",
          description: "program stuff"
        },
        {
          company: "Google",
          jobTitle: "Software Engineer",
          description: "program stuff"
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
      if(this.companyName != null && this.jobTitle != null && this.description != null){
        var newJobApp = { company: this.companyName, jobTitle: this.jobTitle, description: this.description };
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
      if(this.companyName != null && this.jobTitle != null && this.description != null){
        var updatedJobApp = { company: this.companyName, jobTitle: this.jobTitle, description: this.description };
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
    closeUpdateDialog: function(){
      this.updateDialog = false;
      this.companyname = null;
      this.jobTitle = null;
      this.description = null;
      this.$refs.form.reset();
    }
  }
};
</script>
