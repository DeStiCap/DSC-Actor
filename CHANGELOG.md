## [0.3.2-preivew.1] - 2019-05-15
- Change dependency Input System to version 1.0.0

## [0.3.1-preivew.3] - 2019-04-07
- Add IActorData interface.
- Add IActorData_Input interface.
- Change flow from use input from ActorData directly to use from IActorData_Input instead.
- Change Status Data to derived from ScriptableObject.
- Transfer DSC Actor Controller to package.
- Transfer DSC Actor Status to package.
- Remove BaseActorController generic.
- Remove BaseActorStatus generic.

## [0.3.0-preivew.1] - 2019-03-20
- Change all actor behaviour system to new one.
- Move some behaviour to package.
- Remove unused data from template.

## [0.2.10-preivew.2] - 2019-03-18
- Add InputButtonType enum.
- Change all input to support InputButtonType instead InputEventType for support keyboard and joy controller at same time.
- Change dependency Input System to version 1.0.0-preview6
- Remove InputEventType.

## [0.2.9-preivew.2] - 2019-02-23
- Add event helper for add force(3D/2D) for Actor Physic.
- Add event helper for add explosion force for Actor Physic.
- Add event helper time for change Actor time scale.

## [0.2.8-preivew.2] - 2019-02-22
- Add DSC_Actor for use as base Actor Manager.
- Add Actor Manager.
- Add Base Actor Data for use as base of Actor Data class.
- Add time scale multiplier in base actor for use multiplier time scale.(From buff/debuff)
- Change Actor Data to derived from Base Actor Data.
- Change constraint for all generic Actor Data to constraint it as Base Actor Data.

## [0.2.7-preivew.1] - 2019-02-21
- Add Base Actor for use to Base class of Base Actor Controller. It don't have generic variable.
- Add DSC_ActorController_Lite for use with object that don't want to have behaviour system.
- Move Physic Controller to package. (From template)
- DSC Physic now use Base Actor as core to call back when time scale change.

## [0.2.6-preivew.3] - 2019-02-20
- Add Actor Physic System. Use method in this instead Rigidbody. (For support time system.)
- Now 2D side scrolling template full support faster/slower time for physic.
- Update dependency Input System to V1.0.0-preview.5

## [0.2.5-preivew.1] - 2019-02-19
- Change Actor system to support new DSC Time. (Use instead Unity time)
- Now actor can override time scale from specific actor. (Can faster/slower at specific object.)

## [0.2.4-preivew.1] - 2019-02-17
- Now support Input System V1.0.0-preview.5
- Improve Actor Input to support faster/slower time. (About input holding.)

## [0.2.3-preivew.1] - 2019-02-16
- Add Behaviour Condition Group.
- Improve behaviour in 2D side scrolling sample to support pooling data.

## [0.2.2-preivew.1] - 2019-02-15
- Add Damage penetrate flag for use to ignore something when use some attack type.
- Add Damage Behaviour for use to update damage specific behaviour.
- Add Damage Behaviour Type for use to know what damage behaviour type. (For use some case like remove all debuff.)

## [0.2.1-preivew.1] - 2019-02-13
- Add IDamageable to Base Actor script.
- Add BaseActorDamageable for use to damageable system.
- Add BaseActorDamageEvent for use to call damage event on take damage or dead.
- Add OnTakeDamage,OnDead event listener to Actor Damageable.
- Add IFrame Flag for ignore all take damage.
- Change status data to use class instead struct.
- Change DSC_ActorDamageable to derived from BaseActorDamageable.

## [0.2.0-preivew.2] - 2019-02-10
- Add OnInterruptBehaviour to Actor Behaviour.
- Add InterruptBehaviour and InterruptAllBehaviour method to Actor Controller.
- Add support interrupt type to template default and 2D side scrolling.
- Wall Jump now will cancel lock flag when interrupt by damage.
- Now Actor Controller support use real time for update behaviour.
- Now Actor Data in template has time multiplier for multiplier time calculate in behaviour. (For speed up or slow down behaviour. Can't use with add force.)
- Change add force in behaviour to use Impulse mode instead.
- Fix gravity increase behaviour bug.

## [0.1.10-preivew.3] - 2019-02-09
- Now List IAcotrBehaviourData remove method will return true if found and remove data success.
- Now ActorController has running list for running behaviour is list. (Similar old system but check condition still need manual.)
- Add double jump to 2D Side Scrolling template.
- Add wall jump to 2D side Scrolling template.
- Add Actor Flag. It will manage to add/remove flag from Actor Data. (For stack add flag to not remove immediately if not remove by all add flag object.)

## [0.1.9-preivew.2] - 2019-02-08
- Improve 2D Side Scrolling Template to support joycontroller.
- Remove isRunning variable and all of it function. Because ScriptableObject can't have itself data inside. Now need manual check condition.

## [0.1.8-preivew.5] - 2019-02-04
- Add DataRW method to behaviour data for use class instead struct. (For read and write data during run behaviour.)

## [0.1.7-preivew.1] - 2019-01-30
- Add Remove method to extension List IActorBehaviourData.

## [0.1.6-preivew.4] - 2019-01-25
- Add BaseActorInput.
- Add Sample Side Scrolling 2D template.

## [0.1.5-preivew.6] - 2019-01-24
- Add ActorStateFlag for collect Actor current state data.

## [0.1.4-preivew.2] - 2019-01-23
- Change namespace to Actor instead CharacterSystem.
- Add Base Actor Status.

## [0.1.3-preivew.4] - 2019-01-22
- Combine Behaviour set to Behaviour and change it name to Behaviour Group. (Not Divide between set and single anymore.)
- Remove Set Type for Behaviour Group data to use Behaviour Type ID instead.
- Change BaseCharacterController to support behaviour as default.
- Remove abstract method from BaseCharacterController. (Because it support behaviour by itself now.)

## [0.1.2] - 2019-11-17
- Fix fixed update and late update behaviour set not run correctly.

## [0.1.1] - 2019-11-17
- Add meta files for fix error.

## [0.1.0] - 2019-11-17
- Add behaviour set system. It's group pack of behaviour.

## [0.0.6] - 2019-10-31
- Remove template assembly referece.

## [0.0.5] - 2019-10-31
- Try to fix template assembly referece.

## [0.0.4] - 2019-10-31
- Add character template folder in samples.

## [0.0.3] - 2019-10-29
- Add public function to Add/Remove/Get/Set behaviour data in BaseCharacterController.

## [0.0.2] - 2019-10-24
- Fix wrong argument type in BaseCharacterController.

## [0.0.1] - 2019-10-24
- AddBehaviour function now support add list behaviour at sametime.
- Add RemoveAllBehaviour function for remove all behaviour from data.
- Change RemoveBehaviour function to declare remove behaviour type instead use that behaviour directly.

## [0.0.0] - 2019-10-24
- Add character system core scripts.