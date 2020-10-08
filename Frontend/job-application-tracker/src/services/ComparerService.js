const compareJobTitle = (a, b) => {
  if (a.jobTitle < b.jobTitle) {
    return -1;
  } else if (a.jobtTitle > b.jobTitle) {
    return 1;
  } else {
    return 0;
  }
};
const compareCompanyName = (a, b) => {
  if (a.companyName.toLowerCase() < b.companyName.toLowerCase()) {
    return -1;
  } else if (a.companyName.toLowerCase() > b.companyName.toLowerCase()) {
    return 1;
  } else {
    return 0;
  }
};
const compareDateApplied = (a, b) => {
  if (a.dateApplied < b.dateApplied) {
    return -1;
  } else if (a.dateApplied > b.dateApplied) {
    return 1;
  } else {
    return 0;
  }
};
const compareStatusAscending = (a, b) => {
  console.log("sorting by status");
  if (a.status < b.status) {
    return -1;
  } else if (a.status > b.status) {
    return 1;
  } else {
    return 0;
  }
};
const compareStatusDescending = (a, b) => {
  if (a.status > b.status) {
    return -1;
  } else if (a.status < b.status) {
    return 1;
  } else {
    return 0;
  }
};

export {
  compareJobTitle,
  compareCompanyName,
  compareDateApplied,
  compareStatusAscending,
  compareStatusDescending
};
