<template>
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
</template>

<script>
export default {
  name: "add-job-application-dialog",
  props: {
  },
  data() {
    return {
      addDialog: false,
      companyName: null,
      jobTitle: null,
      description: null,
      location: null,
      rules: {
        required: value => !!value || "Required."
      }
    };
  },
  methods: {
    closeAddDialog: function () {
      this.$refs.form.reset();
      this.addDialog = false;
    },
    addJobApplication: function () {
      this.$emit('addJobApplication', this.companyName, this.jobTitle, this.description);
      this.$refs.form.reset();
      this.addDialog = false;
    }
  }
};
</script>