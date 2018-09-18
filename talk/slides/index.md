- title : Probabilistic Programming
- description : Probabilistic programming: how to look like a statistician
- author : Evelina Gabasova
- theme : white
- transition : none

***

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## How to look like a statistician
### A developer's guide to probabilistic programming

<br />
### Evelina Gabašová
### @evelgab
<br />
</div>

------------------------------------------------------------------------------------------------

<img src="images/ati-2.png" />

------------------------------------------------------------------------------------------------

<img src="images/data-science.png" style="height: 600px" />

------------------------------------------------------------------------------------------------

- data-background: images/science-dog.jpg

------------------------------------------------------------------------------------------------

<img src="images/david_cox.jpg" style="height: 600px" />

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Probabilistic programming
<br/>
</div>

' what is probabilistic programming
' It's about writing probability distributions and their manipulations first-class citizens in 
' a programming language
' But why would you use that?

------------------------------------------------------------------------------------------------
- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Probabilistic models
<br/>
</div>


' what are they actually?

------------------------------------------------------------------------------------------------

- data-background: images/developer-survey-2017.png

------------------------------------------------------------------------------------------------

<img src="images/tabs-spaces-blog.png" />

------------------------------------------------------------------------------------------------

<img src="images/tabs-spaces-article2.png" />

------------------------------------------------------------------------------------------------

### Salary distribution

![](images/salary-distribution.png)

------------------------------------------------------------------------------------------------

![](images/low_salary_countries.png)

------------------------------------------------------------------------------------------------

![](images/salary_normal_countries.png)

------------------------------------------------------------------------------------------------

![](images/salary_soviet_countries.png)

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# What's wrong?
<br/>
</div>

------------------------------------------------------------------------------------------------

> What is your current <u>annual</u> salary, in [local currency]? Please enter a whole number in the box below, without any punctuation. If you prefer not to answer, please leave the box empty/blank.

------------------------------------------------------------------------------------------------

![](images/salary_poland.png)

------------------------------------------------------------------------------------------------

# Theory
## People reported their monthly salary

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## Mixture distributions
<br/>
</div>

------------------------------------------------------------------------------------------------

![](images/mixture-distributions-1.png)

------------------------------------------------------------------------------------------------

![](images/mixture-distributions-2.png)

------------------------------------------------------------------------------------------------

![](images/mixture-distributions-3.png)

------------------------------------------------------------------------------------------------

![](images/mixture-distributions-4.png)

------------------------------------------------------------------------------------------------

- data-background : images/bored.gif

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>

# Probability distributions
## in probabilistic programming

<br/>
</div>

' are an integral part of any probabilistic programming language and you can manipulate them

------------------------------------------------------------------------------------------------

' what does it mean?

## Mixture distribution: formally

$$$
\text{Salary} = p \; \mathcal{N}\left(\mu, \sigma^2\right) + \left(1-p\right) \; \frac{1}{12} \; \mathcal{N}\left(\mu, \sigma^2\right)

<br/>

unknown: $ \mu$, $\sigma^2$, $p$

------------------------------------------------------------------------------------------------
- data-background: images/blackboard.png

<h1 style="font-size:1500%; color: white;"> ? </h1>

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Sampling
<br/>
</div>

' is a very efficient way of inferring 

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Sampling: example
## Monty Hall problem
<br/>
</div>

------------------------------------------------------------------------------------------------

# Monty Hall problem

<img src="images/lets-make-a-deal.jpg" style="width: 600px" />

------------------------------------------------------------------------------------------------

![](images/lets-make-a-deal-people.png)

------------------------------------------------------------------------------------------------

![](images/chevrolet.png)

------------------------------------------------------------------------------------------------

<img src="images/lets-make-a-deal.jpg" style="width: 800px" />

------------------------------------------------------------------------------------------------

<img src="images/monty-hall-1.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

<img src="images/monty-hall-2.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

<img src="images/monty-hall-3.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

<img src="images/monty-hall-4.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## Demo
### Monte Carlo sampling
<br />
</div>

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## Demo
### Representing probability distributions
### with computation expressions
<br/>
</div>

------------------------------------------------------------------------------------------------

## Mixture distribution: formally

$$$
\text{Salary} = p \; \mathcal{N}\left(\mu, \sigma^2\right) + \left(1-p\right) \; \frac{1}{12} \; \mathcal{N}\left(\mu, \sigma^2\right)

<br/>

unknown: $ \mu$, $\sigma^2$, $p$

------------------------------------------------------------------------------------------------

## Mixture distribution: informally

$$$
\text{Salary} = p(\text{correct}) \,\times\, \text{Annual salary} + \\
 \;\;\;\;\;\;\;\;\; + \, p\left(\text{mistake}\right) \; \frac{1}{12} \,\times\, \text{Annual salary}

<br/>

unknown: $ p(\text{correct})$, Annual salary

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Inference 
## for mixture distributions
<br/>

Easy if we knew the values of the unknown parameters
<br/><br/>
</div>

------------------------------------------------------------------------------------------------

## Mixture distributions
### Probabilistic programming

	let salary = Gaussian(mean,variance)
	let mistake = Bernoulli(probability)

	let observed =
	    if mistake then
	        1/12 * salary
	    else
	        salary

' much simpler
' This is an example of a potential probabilistic programming language
' Manipulating probability distributions directly

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## Demo
### Modelling probability distributions with computation expressions
<br/>
</div>

------------------------------------------------------------------------------------------------

# Modelling 
## mixture distributions

But how do we get the parameters?

------------------------------------------------------------------------------------------------

<img src="images/probabilistic-model.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

<img src="images/inference.png" style="height: 600px" />

' You get PhD from Cambridge for this stuff
' This is exactly why people use probabilistic programming - because you don't have to know how 
' it works underneath

------------------------------------------------------------------------------------------------

## The world's slowest probability inference engine

![](images/difference-engine.gif)

------------------------------------------------------------------------------------------------

- data-background : images/mixture-of-gaussians-parameters.png
- class : withbackground

<h1> complete enumeration </h1>

------------------------------------------------------------------------------------------------

![](images/salary-distribution.png)

------------------------------------------------------------------------------------------------

### Try different parameter values

![](images/enumeration_density.png)

------------------------------------------------------------------------------------------------

### Discretize

![](images/enumeration_discrete.png)


------------------------------------------------------------------------------------------------

### Compare two discrete distributions

<table>
<tr> <td> 
<img src="images/actual_density.png" />
</td>
<td>
<img src="images/enumeration_discrete-1.png" />
</td></tr>
</table>

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
## Demo
### The world's slowest probabilistic language
<br/>
</div>

------------------------------------------------------------------------------------------------

<img src="images/estimated-distribution.png" style="width: 1000px" />

------------------------------------------------------------------------------------------------

- data-background: images/blackboard.png

<div style="background:white;">
<br/>
# Probabilistic programming
## in the real world
<br/>
</div>

------------------------------------------------------------------------------------------------

- data-background : black


		data {
		  int<lower = 0> N;
		  vector[N] y;
		}

		parameters {
		  vector[2] mu;
		  real<lower=0> sigma[2];
		  real<lower=0, upper=1> theta;
		}

		model {
		  sigma ~ normal(0, 2);
		  mu ~ normal(0, 2);
		  theta ~ uniform(0.0,1.0);
		  for (n in 1:N)
		    target += log_mix(theta,
		      normal_lpdf(y[n] | mu[1], sigma[1]),
		      normal_lpdf(y[n] | mu[2], sigma[2]));
		}

------------------------------------------------------------------------------------------------

- data-background: images/stock-photo-mathematician.jpg
  
------------------------------------------------------------------------------------------------

- data-background : images/blackboard.png

<table>
<tr>
  <td class="noborder" style="width:60%;"></td>
   <td class="noborder" style="width:40%; background: white;">

<h1> Evelina Gabašová </h1>
<h3 >
@evelgab </h1>
<h3>evelinag.com</h1><br />
<br /><br /><br/><br/><br/><br/>
</td> 
</tr>
</table>
