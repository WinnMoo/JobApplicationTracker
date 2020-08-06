<template>
  <v-dialog persistent max-width="600px" ref="dialog">
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
</template>

<script>
export default {
  name: "update-job-application-dialog",
  props: {
  },
  data() {
    return {
      rules: {
        required: value => !!value || "Required."
      }
    };
  },
  methods: {}
};
</script>