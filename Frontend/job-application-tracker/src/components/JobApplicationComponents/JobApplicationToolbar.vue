<template>
  <v-toolbar dense>
    <v-select
      prepend-inner-icon="mdi-filter-variant"
      v-model="sortType"
      :items="sortingMethods"
      item-text="sortMethod"
      item-value="sortMethod"
      label="Sort by:"
      dense
      outlined
      style="top: 13px"
    >
    </v-select>
    <div class="mx-4"></div>
    <v-btn-toggle v-model="viewType" color="primary" dense group>
      <v-btn :value="1" text>
        <v-icon>mdi-menu</v-icon>
      </v-btn>
      <v-btn :value="2" text>
        <v-icon>mdi-format-align-justify</v-icon>
      </v-btn>
    </v-btn-toggle>
  </v-toolbar>
</template>

<script>
export default {
  name: "JobApplicationToolbar",
  props: {
    viewTypeProp: Number
  },
  data() {
    return {
      sortingMethods: [
        { sortMethod: "Date Applied" },
        { sortMethod: "Status (Ascending)" },
        { sortMethod: "Status (Descending)" },
        { sortMethod: "Job Title" },
        { sortMethod: "Company Name" }
      ],
      sortType: { sortMethod: "Date Applied" },
      viewType: 1
    };
  },
  watch: {
    sortType: function() {
      this.sortJobApplications(this.sortType);
    },
    viewType: function() {
      this.changeViewType();
    }
  },
  methods: {
    sortJobApplications: function() {
      this.$emit("sortJobApplications", this.sortType);
    },
    changeViewType: function () {
      this.$emit("changeViewType", this.viewType);
    }

  }
};
</script>
