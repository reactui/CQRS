describe('Has UI started at all', () => {
  it('Visits the initial project page', () => {
    cy.visit('/')
    cy.contains('Manage Contacts')    
  })
})

describe('Second test', () => {
  it('Clicks on the edit button of the first grid row', () => {
    cy.visit('/')    
    
    let element = cy.get('.pi-pencil').eq(0);
    element.click();

    cy.contains('Contact Details');
  })
})
