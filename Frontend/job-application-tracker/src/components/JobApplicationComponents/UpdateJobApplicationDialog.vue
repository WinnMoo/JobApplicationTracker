<template>
  <v-dialog :value="this.showDialog" persistent max-width="600px" ref="dialog">
    <v-card>
      <v-form ref="form">
        <v-card-title>
          <span class="headline">Update Job Application</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  :rules="[rules.required]"
                  label="Company Name*"
                  v-model="companyNameToUpdate"
                  hint="E.g. Microsoft"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  :rules="[rules.required]"
                  label="Job Title*"
                  v-model="jobTitleToUpdate"
                  hint="E.g. Software Engineer"
                  required
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-textarea
                  label="Description"
                  v-model="descriptionToUpdate"
                ></v-textarea>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="updateJobApplication(false)"
            >Close</v-btn
          >
          <v-btn color="blue darken-1" text @click="updateJobApplication(true)"
            >Save</v-btn
          >
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "UpdateJobApplicationDialog",
  props: {
    jobTitle: String,
    companyName: String,
    description: String,
    updateDialog: Boolean,
    idToUpdate: String
  },
  watch: {
    updateDialog: {
      handler(oldVal, newVal) {
        this.showDialog = oldVal;
        return newVal;
      }
    },
    companyName: {
      handler(oldVal, newVal) {
        this.companyNameToUpdate = oldVal;
        return newVal;
      }
    },
    jobTitle: {
      handler(oldVal, newVal) {
        this.jobTitleToUpdate = oldVal;
        return newVal;
      }
    },
    description: {
      handler(oldVal, newVal) {
        this.descriptionToUpdate = oldVal;
        return newVal;
      }
    }
  },
  data() {
    return {
      showDialog: this.updateDialog,
      location: null,
      jobTitleToUpdate: this.jobTitle,
      companyNameToUpdate: this.companyName,
      descriptionToUpdate: this.description,
      rules: {
        required: value => !!value || "Required."
      }
    };
  },
  methods: {
    updateJobApplication: function(dialogCondition) {
      this.$emit(
        "updateJobApplication",
        this.companyNameToUpdate,
        this.jobTitleToUpdate,
        this.descriptionToUpdate,
        this.idToUpdate,
        dialogCondition
      );
      this.$refs.form.reset();
      this.showDialog = false;
    }
  }
};
</script>
