import React, { useEffect } from 'react'
import { Card } from 'react-bootstrap'

const CardsComponent = ({order}) => {
    useEffect(()=>{
       // console.log("component",order)
    },[])
  return (
    <Card className="mb-5 shadow-sm col-3" style={{ width: 'max-content', borderRadius: '10px', overflow: 'hidden' }}>
    <Card.Header className="bg-primary text-white text-center">
      <Card.Title>Order</Card.Title>
    </Card.Header>
    <Card.Body style={{display:"flow"}}>
      <p><strong>OrderId:</strong> {order.Id}</p>
      <p><strong>Origin Node:</strong> {order.OriginNodeId}</p>
      <p><strong>Target Node:</strong> {order.TargetNodeId}</p>
      <p><strong>Load:</strong> {order.Load}</p>
      <p><strong>Value:</strong> {order.Value}</p>
      <p><strong>Delivery Date (UTC):</strong> {new Date(order.DeliveryDateUtc).toLocaleString()}</p>
      <p><strong>Expiration Date (UTC):</strong> {new Date(order.ExpirationDateUtc).toLocaleString()}</p>
    </Card.Body>
  </Card>
  )
}

export default CardsComponent